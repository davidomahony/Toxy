# Toxy
Just a POC for something I am building


## TODO

Main Service
- Authentication
- Other Tokens
    - Email
    - Phone number
- Repository per token type
- Bit un tidy around identifers
- Instead of having count in the token, I need a string
- AES encryption
- RSA Encryption
- I need caches, so slow
- Restrictions on setting tenant configuration, token names need to be gated as this will create a new collection.  
    No they dont, same tokens can be shared across configs
- Likely need terraform to show this being built