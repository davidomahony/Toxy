# Encryption Options
Lets go through the various options and the challenges imposed by each one, really there is only two. Also need to go into which algorithms. Lets use something crap first incase anyone is actually tracking this.

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







