# ChatChallenge [![Run Unit](https://github.com/alvinmarshall/ChatChallenge/actions/workflows/pull-request-build.yaml/badge.svg)](https://github.com/alvinmarshall/ChatChallenge/actions/workflows/pull-request-build.yaml)

Assignment
---
The goal of this exercise is to create a simple browser-based chat application using .NET.
This application should allow several users to talk in a chatroom and also to get stock quotes from an API using a specific command.

Mandatory Features
---
* Allow registered users to log in and talk with other users in a chatroom.
* Allow users to post messages as commands into the chatroom with the following format
/stock=stock_code
* Create a decoupled bot that will call an API using the stock_code as a parameter
[API Link Here](https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv), here aapl.us is the
stock_code)
* The bot should parse the received CSV file and then it should send a message back into
the chatroom using a message broker like RabbitMQ. The message will be a stock quote using the following format: “APPL.US quote is $93.42 per share”. The post owner will be the bot.
* Have the chat messages ordered by their timestamps and show only the last 50 messages.
* Unit test the functionality you prefer.
  
Bonus (Optional)
---
* Have more than one chatroom.
* Use .NET identity for users authentication
* Handle messages that are not understood or any exceptions raised within the bot.
* Build an installer.

Considerations
---
* We will open 2 browser windows and log in with 2 different users to test the functionalities.
* The stock command won’t be saved on the database as a post.
* The project is totally focused on the backend; please have the frontend as simple as you
can.
* Keep confidential information secure.
* Pay attention if your chat is consuming too many resources.
* Keep your code versioned with Git locally.
* Feel free to use small helper libraries

Deliverables
---
When you finish the assignment, send a zip file (don’t forget to include the .git/ folder.) or upload your project to your Git repo (Github, BitBucket, etc... and share the repository link with your initial contact via email. Indicate which, if any, of the bonus tasks you completed.
If you didn’t manage to finish everything, please tell us which parts you completed.
