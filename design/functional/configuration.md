# Configuration

I want one simple configuration to rule them all, but as there is so many moving parts we need to split them up per component. Right now we can assume we have two components. Each component has its own configuration with a main configuration which looks after general rules also.

## Core
This will be the main configuration, looks after the various information relating to the tokens, which are app agnostic. Parts of this will need to be fetched from a keyvault
- Number of tokens
- Token type
- Services

```json

{
    "tokenizationMethods":
    [
        {
            "name": "string",
            "id": "guid",
            "methodUsed": "",
            "key" : "", // key vault
            "salt": "", // key vault
            "className": "string",
            "propertyName": "string",
            "otherIdentifier": "string",
            "dataType": "string",
            "tokenRegexDetector": "(-)(\\w{2})(.*?)(-\\*)",
            "preWrapper": "<" ,
            "postWrapper": ">" ,
        }
    ],
    "services":
    [
        {
            "name": "string",
            "id": "guid",
            "url": "", // key vault
            "iprange": "", // key vault
            "readAccess" : [
                "allowedUpn" // again key vault
            ],
            "writeAccess": [
                "allowedUpn" // again key vault
            ]
        }
    ]
  ]
}

```

I think this configuration is a bit complex, lets see if we can simplify it

Basic Core Flow Tokenization

Value comes in with             Id looks up                 Tokenization Method                 Token Generated
tokenization method ------>     configuration       ------> Fetched From Config     ------>      & Returned
& ConfigurationId    


Basic Core Flow Detokenization

Token comes in with             Id looks up                 Tokenization Method                 Token Parsed 
tokenization method ------>     configuration       ------> Fetched From Config     ------>      & Value Returned
& ConfigurationId    

Proxy How do we identify a token?
1. We need a regex to identify tokens.
    - If we do this tokens need to have an easy regex to detect its a token
    - We also need to detect what token type it is, string & date
    - We need to detect what type of string it is
    ---> |TokenStart|TokenSubTypeId|TokenMappingValue|TokenEnd|

    - A regex would be needed to idenitfy all parts and extract.

    - Some example code execution stages for detokenization
        - Look up configuration
            Fail all if missing
        - Look up tokenization method
            Fail just that token if missing
        - Use the regex pattern for that method to get the identifier and token value
        - Look up token value against identifier table and return


    - Some example code execution stages for tokenization
        - Look up configuration
            Fail all if missing
        - Look up tokenization method
            Fail just that token if missing
        - Depends on the tokenization type
        - CHeck if exists or tokenize



2. We need each response mapped so we fetch tokens.
    - This sounds like death by configuration
    - Also we will have more reads then writes




## Proxy
- Endpoints
- Fields
- How to detect tokens





