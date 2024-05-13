# Boardgames EShop Management

A web API developed during an internship of 3 months following technical guidance, with almost 0 supervision and no previous knowledge about .NET (before this I worked with JavaScript, SQL and some Python).

During this period, the following endpoints became available for the user:
- create, read, update and delete the account
- sort boardgames by release year, price or name
- filter boardgames by name, category and/or page number
- review a boardgame
- create wishlist with boardgames, edit wishlist name or delete its items and delete wishlist
- order boardgames
- soft delete functionality (anonymize data and archive instead of hard delete)

And as an admin:
- delete an account
- add, edit or delete a boardgame and its category
- delete a review
- update order status

Built with .NET 6, SQL Server and Blob Storage. 
The following packages were used: 
- Entity Framework ORM, MediatR, AutoMapper, Bogus, Microsoft Identity and Azure Storage Blobs

The development lasted until October 2022 and from then on I sometimes come back to the application and use it as a sandbox.

### Notes

Blob Storage is no longer working due to account trial. <br />
KeyVault was not used because the focus wasn't on the cloud.
