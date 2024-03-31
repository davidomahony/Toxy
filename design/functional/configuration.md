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
}

```


## Proxy
- Endpoints
- Fields
- How to detect tokens





