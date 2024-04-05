# Auditing & Reporting
As one of them main features of this solution is to be able monitor data usage auditing various requests plays a big role in a successful product.


## When to audit
Ideally a tenant will want to be able to get visibility over which user various data interactions such as:
1. Tokenizing Data
    - Success
    - Failures
    - Access Issues
    - Time/performance
2. Detokenizing Data
    - Success/User
    - Access Results
    - Time/performance

Various reports should be made available to a tenant. Ideally this could be done by exposing a query endpoint which allows tenants to build there own reports.

## How is it available
As this informaiton will likely be gethered by scraping logs, perhaps this should be specified but then generated using a background job?



