# Encryption Options
Lets go through the various options and the challenges imposed by each one, really there is only two.

## Deterministic
As we will always get the same encrypted value for the same clear text value. We can use multiple iterations of this to give us somenthing more secure and potentially salting.

### Advantage
- Prevents database getting exponentially bigger

### Disadvantage
- Not as secure


## Non Deterministic
This is without a doubt the most secure the element of randomness keeps giving us different results from the same value. But this can also result is a great deal of tokens being created for the same value. Which causes the data store to grow exponentially larger as we add more values.

I really dont think we can track these values at all, in terms of checking for duplicates as a result these should be for items which have high level of security.

### Advantage 
- Security

### Disadvantage
- Causes the database to get exponentially bigger





## Problems (This should probably be in a different file)
We need a way to clean up tokens

- So option one is we make a hot or cold path, but this gets tricky, when do we look in the cold path?
- What about a mechanism to see if tokens should be purged, so we need to see when reads occur followed.

- Maybe an event hook to archive tokens, but deleting tokens is kind of risky straight away, perhaps a cool down process before deletion is better.

- Perhaps rules around how a token transitions from a hot path to a cold, which state if a token has not been used in x amount of time it is sent to this cold path which is eventually deleted. The best way to gaurd PII is to delete it

- This idea of a yearly purge would work as well its just very intrusive.
    - The idea of a daily or monthly job which runs to see which tokens exist in the customer database this basically identifies tokens which exist in my vaul but not in the customers data stores. In turn these are then invalidated in the vault for a period of time before being deleted. We need to audit all these.
- A web hook idea of an on delete event is nice also, but intrusive






