# Toxy
Just a POC for something I am building


## TODO

Main Service
- Authentication
- Other Tokens
    - Email
    - Phone number
- Repository per token type (done)
- Bit un tidy around identifers
- Instead of having count in the token, I need a string
- AES encryption (done)
- RSA Encryption (done)
- I need caches, so slow
- Restrictions on setting tenant configuration, token names need to be gated as this will create a new collection.  
    No they dont, same tokens can be shared across configs
- Likely need terraform to show this being built
- Need restrictions on padding byte to identify token


# MVP to do list
## Multi Tenant Approach
- String Tokenization
- Card Values (S)
- Authentication
    - Nearly there need to get claims back in the token
- Logging/Telemetry
- Potential use of ai for detecting anomalies
- Configuration Validation
    - Can be slower if it envolves getting a more valid, read write frequency is a bit slower so that bit.
- Security Pieces
    - KeyVault

FE - Need a way to create configuration

Documentation
- Security 
- Configuration
- Setup

Test Coverage 100%

CI/CD
Pipelines

Would need some serious terraform scripts,

## On Premises Approach
- All multi tenant requirements
- Infra needs faremore work
    - Specific DB's
    - AKS 
    - Private link
