# OpenAI Detective Game
 A thrilling detective game where players utilize a chatbot AI, to interrogate an AI-generated chatbot in order to gather information and solve a complex crime case. Players will engage in conversations, ask questions, and uncover clues to progress in their investigation.
 
# How to Import
Clone or download the repository to your local machine.
Open Unity 2019 or later.

Setting Up Your OpenAI Account
To use the OpenAI API, you need to have an OpenAI account. Follow these steps to create an account and generate an API key:

Go to https://openai.com/api and sign up for an account.
Once you have created an account, go to https://beta.openai.com/account/api-keys.
Create a new secret key and save it.
Saving Your Credentials
To securely store your OpenAI API key and organization name (if applicable), follow these steps:

Create a folder called .openai in your home directory. For example:
Windows: C:\User\UserName\
Linux or Mac: ~\
Create a file called auth.json in the .openai folder.
Add the following fields to the auth.json file and save it:
json
Copy code
{
    "api_key": "YOUR_API_KEY",
    "organization": "YOUR_ORGANIZATION_NAME"
}
Replace YOUR_API_KEY with your OpenAI API key and YOUR_ORGANIZATION_NAME with your organization name (if applicable).
IMPORTANT: Treat your API key as a secret and do not share it with others or expose it in client-side code (e.g., browsers, apps). If you are using OpenAI for production, securely load your API key from an environment variable or key management service on the server-side.

Please ensure that you follow the security practices and guidelines provided by OpenAI to protect your API key.

# How to Play
Enter questions into the text input field in the Main scene and await a response from the AI. Play along and try to give evidence and get the AI to admit that it comitted the crime.
