# Configuration

V1 configuration was overly complex for what it needed, lets fix this and keep it simple stupid

### Lets consider some flows 

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

Previous configuration was too messy, it needs to be simplified



2. We need each response mapped so we fetch tokens.
    - This sounds like death by configuration
    - Also we will have more reads then writes


```json

{
    "tokenizationMethods":
    [
        {
            "name": "string", // friendly name
            "id": "int", // used to identify tokens

            "key" : "string", // string stored in key vault
            "salt": "string", // string stored in key vault
            "encryptionType": "enum", // Enum based on whats available
            "methodUsed": "enum", // Enum: Date, String, CardNumber etc

            "preWrapper": "<" ,
            "postWrapper": ">" ,
            "padIdentifier": "string", // identifies token
            "tokenDetectorRegex": "string", // used to detect a token in a large text file
            "tokenParsingRegex": "string", // used to split a token into --> |TokenStart|TokenSubTypeId|TokenMappingValue|TokenEnd|
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


## Technology to use

I am stuck between Postgres or Mongo

- Postgres
Look this configuration is not going to scale, perhaps a basic postgres table would take it. And it would give me the option of having some json strings as columns to give me an element of unstructured data, as a few unknowns still

- Mongo
Look easy to use unstructured and I have it more or less done already. I really dislike doing double reads to get the value I just updated. Would have to do it with Postgres too. But getting the sense I am using a hammer where to do the job of a sewing needle.