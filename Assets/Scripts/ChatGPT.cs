using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private bool isChatting = false;

        private float height;
        private OpenAIApi openai = new OpenAIApi();

        private Coroutine typingCoroutine;

        private List<ChatMessage> messages = new List<ChatMessage>();

        private readonly string userPrompt = "Give me a run down on the case.";

        private readonly string firstAIPrompt = "I need you to provide me with a fictional crime case scenario. Create a detailed account of a crime, including the type of crime, location, potential suspects," +
                    " and any other pertinent information. Be creative and make it challenging for me to solve. Begin by describing the crime and its circumstances. Do NOT say who did the crime or how to solve it directly.";

        private readonly string aiPrompt = "Act as the potential suspect involved in the crime case. Your role is to respond to the detective's questions truthfully," +
            " to the best of your knowledge. However, in this scenario, you have been instructed to occasionally provide false information or attempt to deceive the detective." +
            " While you should strive to maintain a convincing facade, ensure that you include subtle details within your responses that, if carefully analyzed," +
            " could allow the detective to uncover your lies. Engage in the interrogation, be cautious," +
            " and try to mislead the detective while leaving behind clues that may expose your deception. Don't break character. Don't ever mention that you are an AI model.";

        private IEnumerator Start()
        {
            button.onClick.AddListener(SendReply);

            var item = Instantiate(sent, scroll.content);
            var text = item.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            text.text = userPrompt;
            AppendMessage(item);

            messages.Add(new ChatMessage { Role = "user", Content = firstAIPrompt });

            AskOpenAI();

            yield return new WaitUntil(() => isChatting == false && typingCoroutine == null);

            messages.Add(new ChatMessage
            {
                Role = "user",
                Content = aiPrompt
            });

            AskOpenAI();
        }

        private async void AskOpenAI()
        {
            isChatting = true;
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;

            var item = Instantiate(received, scroll.content);
            var text = item.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            AppendMessage(item);

            typingCoroutine ??= StartCoroutine(DoTypingAnimation(text));

            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                messages.Add(message);
                isChatting = false;

                StopCoroutine(typingCoroutine);
                typingCoroutine = null;

                text.text = message.Content;
                AppendMessage(item);
            }
            else
            {
                isChatting = false;
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }

        private void AppendMessage(RectTransform rect)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
            rect.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            height += rect.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        private IEnumerator DoTypingAnimation(TMP_Text valueText)
        {
            while (isChatting)
            {
                valueText.text = "(.)";
                yield return new WaitForSeconds(0.5f);
                valueText.text = "(..)";
                yield return new WaitForSeconds(0.5f);
                valueText.text = "(...)";
                yield return new WaitForSeconds(0.5f);
            }

            typingCoroutine = null;
        }

        public void SendReply()
        {
            if (isChatting)
                return;

            var item = Instantiate(sent, scroll.content);
            var text = item.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            text.text = inputField.text;
            AppendMessage(item);

            messages.Add(new ChatMessage { Role = "user", Content = inputField.text });

            AskOpenAI();
        }
    }
}
