using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using OpenAI;

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

    private readonly string aiPrompt = "Act as the suspect in a fictional crime case and reply to the questions, while occasionally lying and giving hints that you are the one who comitted the crime, without actually saying so. Don't break character. Don't ever mention that you are an AI model. DO NOT speak for the detective. Only answer questions asked. Slowly get more afraid after each question and eventually confess if the detective gets enough information to prove you did it.";

    private void Start() => button.onClick.AddListener(SendReply);

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
            Model = "gpt-3.5-turbo",
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
            isChatting = false;

            StopCoroutine(typingCoroutine);
            typingCoroutine = null;

            text.text = "Message failed to generate, please retry.";
            AppendMessage(item);
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

    public void SendReply()
    {
        if (isChatting || inputField.text == string.Empty)
            return;

        var item = Instantiate(sent, scroll.content);
        var text = item.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        text.text = inputField.text;
        AppendMessage(item);

        messages.Add(new ChatMessage { Role = "user", Content = aiPrompt + "\n" + inputField.text});

        AskOpenAI();
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
}
