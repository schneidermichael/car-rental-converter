[![.NET](https://github.com/schneidermichael/car-rental-converter/actions/workflows/dotnet.yml/badge.svg)](https://github.com/schneidermichael/car-rental-converter/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![codecov](https://codecov.io/gh/schneidermichael/car-rental-converter/branch/master/graph/badge.svg?token=ACQDGY3BR2)](https://codecov.io/gh/schneidermichael/car-rental-converter)
## Getting Started

1. `git clone https://github.com/schneidermichael/car-rental-converter.git` 
2. `docker pull michaelxschneider/carrentalconverter:0.0.3`
3. `docker run --name car-rental-converter -d -p 49174:80 michaelxschneider/carrentalconverter:0.0.3`
4. Open [Postman](https://www.postman.com/downloads/)
5. Go to **New**
6. Select **gRPC Requeest**
7. Enter server URL -> `localhost:49174` or `0.0.0.0:49174` (depends on OS and your individuel Host configuration)
8. Choose a way to load services and methods -> More ways to load services and methods -> Import protobuf definition from file -> Choose File -> `car-rental-converter/Protos/currency_converter_service.proto` -> Next -> Use without importing
9.  Select a method -> CurrencyConverter/Login
10. Use input from Login 
11. Invoke -> works only if Container is RUNNING :) 

## Methods

* Login
* ListOfCurrencies *(Authorization-Bearer Token)*
* CurrencyPerSymbol *(Authorization-Bearer Token)*
* CalculatingCrossCurrency *(Authorization-Bearer Token)*

## Examples for Request/Response

### Login

#### Input

`{
    "password": "car",
    "username": "group1"
}`

#### Output

`{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiZ3JvdXAxIiwiZXhwIjoxNjQ4Mzc3OTAyLCJpc3MiOiJjYXItcmVudGFsLWNvbnZlcnRlciIsImF1ZCI6ImNhci1yZW50YWwtYXBwIn0.ts__XgMgDB8DETbvVKMfFc4LhP6Sc_pXIhvp6k08CZw"
}`
### ListOfCurrencies *(Authorization-Bearer Token)*

#### Input

`{}`

#### Output

`{
    "currencies": [
        {
            "symbol": "USD",
            "rate": 1.1002
        },
        {
            "symbol": "JPY",
            "rate": 134.07
        },
        {
            "symbol": "BGN",
            "rate": 1.9558
        },
        {
            "symbol": "CZK",
            "rate": 24.645
        },
        {
            "symbol": "DKK",
            "rate": 7.4404
        },
        {
            "symbol": "GBP",
            "rate": 0.8338
        },
        {
            "symbol": "HUF",
            "rate": 373.81
        },
        {
            "symbol": "PLN",
            "rate": 4.7307
        },
        {
            "symbol": "RON",
            "rate": 4.9487
        },
        {
            "symbol": "SEK",
            "rate": 10.3505
        },
        {
            "symbol": "CHF",
            "rate": 1.0207
        },
        {
            "symbol": "ISK",
            "rate": 142.2
        },
        {
            "symbol": "NOK",
            "rate": 9.5205
        },
        {
            "symbol": "HRK",
            "rate": 7.5754
        },
        {
            "symbol": "TRY",
            "rate": 16.3304
        },
        {
            "symbol": "AUD",
            "rate": 1.4624
        },
        {
            "symbol": "BRL",
            "rate": 5.2634
        },
        {
            "symbol": "CAD",
            "rate": 1.3781
        },
        {
            "symbol": "CNY",
            "rate": 7.0007
        },
        {
            "symbol": "HKD",
            "rate": 8.6117
        },
        {
            "symbol": "IDR",
            "rate": 15777.69
        },
        {
            "symbol": "ILS",
            "rate": 3.5351
        },
        {
            "symbol": "INR",
            "rate": 83.8235
        },
        {
            "symbol": "KRW",
            "rate": 1343.32
        },
        {
            "symbol": "MXN",
            "rate": 21.9908
        },
        {
            "symbol": "MYR",
            "rate": 4.6324
        },
        {
            "symbol": "NZD",
            "rate": 1.5787
        },
        {
            "symbol": "PHP",
            "rate": 57.322
        },
        {
            "symbol": "SGD",
            "rate": 1.4919
        },
        {
            "symbol": "THB",
            "rate": 36.906
        },
        {
            "symbol": "ZAR",
            "rate": 16.0386
        }
    ]
}`

### CurrencyPerSymbol *(Authorization-Bearer Token)*

#### Input

`{
    "symbol": "USD"
}`

#### Output 

`{
    "curreny": {
        "symbol": "USD",
        "rate": 1.1002
    }
}`

### CalculatingCrossCurrency *(Authorization-Bearer Token)*

#### Input

`{
    "amount": 1.00,
    "symbol_input": "EUR",
    "symbol_output": "USD"
}`

#### Output

`{
    "symbol": "USD",
    "result": 1.1002
}`

