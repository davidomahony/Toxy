# Purging Tokens
We need a plan for how we will purge or remove stale tokens.

## Job run a cadence


## Demotion ftera time period


## Notification of deleted tokens


## Problems (This should probably be in a different file)
We need a way to clean up tokens

- So option one is we make a hot or cold path, but this gets tricky, when do we look in the cold path?
- What about a mechanism to see if tokens should be purged, so we need to see when reads occur followed.

- Maybe an event hook to archive tokens, but deleting tokens is kind of risky straight away, perhaps a cool down process before deletion is better.

- Perhaps rules around how a token transitions from a hot path to a cold, which state if a token has not been used in x amount of time it is sent to this cold path which is eventually deleted. The best way to gaurd PII is to delete it

- This idea of a yearly purge would work as well its just very intrusive.
    - The idea of a daily or monthly job which runs to see which tokens exist in the customer database this basically identifies tokens which exist in my vaul but not in the customers data stores. In turn these are then invalidated in the vault for a period of time before being deleted. We need to audit all these.
- A web hook idea of an on delete event is nice also, but intrusive

