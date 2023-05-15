# OpenAI Detective Game

A thrilling detective game where players utilize a chatbot AI to interrogate an AI-generated chatbot in order to gather information and solve a complex crime case. Players will engage in conversations, ask questions, and uncover clues to progress in their investigation.

## How to Import

1. Clone or download the repository to your local machine.
2. Open Unity 2019 or later.

## Setting Up Your OpenAI Account

To use the OpenAI API, you need to have an OpenAI account. Follow these steps to create an account and generate an API key:

1. Go to [https://openai.com/api](https://openai.com/api) and sign up for an account.
2. Once you have created an account, go to [https://beta.openai.com/account/api-keys](https://beta.openai.com/account/api-keys).
3. Create a new secret key and save it.

## Saving Your Credentials

To securely store your OpenAI API key and organization name (if applicable), follow these steps:

1. Create a folder called `.openai` in your home directory. For example:
   - Windows: `C:\User\UserName\`
   - Linux or Mac: `~\`
2. Create a file called `auth.json` in the `.openai` folder.
3. Add the following fields to the `auth.json` file and save it:
   ```json
   {
       "api_key": "YOUR_API_KEY",
       "organization": "YOUR_ORGANIZATION_NAME"
   }
   
Replace `YOUR_API_KEY` with your OpenAI API key and `YOUR_ORGANIZATION_NAME` with your organization name (if applicable).

IMPORTANT: Treat your API key as a secret and do not share it with others or expose it in client-side code (e.g., browsers, apps). If you are using OpenAI for production, securely load your API key from an environment variable or key management service on the server-side.

Please ensure that you follow the security practices and guidelines provided by OpenAI to protect your API key.

# How to Play

1. Enter questions into the text input field in the Main scene.
2. Await a response from the AI.
3. Play along and try to provide evidence and get the AI to admit that it committed the crime.

## OpenAI Wrapper

For integration with the OpenAI API, this project utilizes the [OpenAI Unity wrapper](https://github.com/srcnalt/OpenAI-Unity/tree/master) 

Thanks to the author for their contribution to this project.

Please refer to the [OpenAI Unity wrapper repository](https://github.com/srcnalt/OpenAI-Unity/tree/master) for detailed documentation and usage instructions.

## About This Game

This game was developed in just one day as part of a sudden idea. While the visuals and graphics may not be polished or visually stunning, the emphasis of this project lies in the gameplay and narrative experience.

The visual quality may not meet professional standards, but the focus was placed on creating engaging and immersive text-based interactions. The strength of the game lies in the compelling dialogue and intricate crime-solving puzzles. If you are also to create your own prompt in ChatGPT.cs, you can easily create many different games also.

Please note that this project is a prototype and may not receive further updates or improvements beyond its initial development stage.
