# CurrencyTradingEngine
DDD Sample

REQUIREMENTS:
1. User should be able to send money to other users
2. User should be able to store money in his balance in different currencies and also exchange his money between them
3. User should be able to check his own balance

The application uses In Memory DB so data is not persisted. The application focuses only the 3 functionalities above so the users and currencies need to be initialized every run 

To initialize users and currencies:
POST http://localhost:10211/api/user/init-users

To check all users and balances (call this to see updated value):
GET http://localhost:10211/api/user/list

To check individual balance:
GET http://localhost:10211/api/money/balance?token=User1

To send money from user1 to user2:
POST http://localhost:10211/api/money/send?token=User1
JSON payload:{"ToUser":"User2", "CurrencyName": "USD", "Amount": 50.0}

To Exchange user balance from one currency to another:
http://localhost:10211/api/money/exchange?token=User1
JSON payload: {"FromCurrencyName":"USD", "ToCurrencyName": "PHP", "Amount": 50.0}

Initialized Users:
UserId: 1 UserName: "User1" Default Balance: 100 USD
UserId: 1 UserName: "User1" Default Balance: 100 USD
