# TwitterPostOAuthAuthorization 🔐🐦

A C# console application that uses Twitter's **3-legged OAuth 1.0a flow** to authenticate a user and post a tweet on their behalf using the Twitter API v2.

---

## 📌 What It Does

This app demonstrates the full OAuth flow:

1. Redirects the user to Twitter to authorize the app.
2. Receives a verifier code.
3. Exchanges it for access tokens.
4. Posts a tweet using the authenticated user's account.

---

## 🚀 Technologies Used

- .NET 6 / .NET Core
- [Tweetinvi](https://github.com/linvi/Tweetinvi) (C# Twitter API library)
- Twitter API v2

---

## 🔐 Requirements

To run this, you need:

- Twitter Developer Account
- Twitter API credentials:
  - API Key
  - API Secret Key
  - Callback URL (e.g. `https://127.0.0.1:8000`)


