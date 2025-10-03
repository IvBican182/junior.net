Controlleri su uredno testirani, pokušao sam kreirati što sličniji slučaj stvarnoj aplikaciji

- clone repo
- dotnet build
- podesiti connection string
- Add-Migration u CPM-u
- update-database
- dotnet build, dotnet run

- Prije isprobavanja controllera za Narudžbe (Order) potrebno je popuniti tablice vrijednostima, evo par insertova da ubrzamo proces:


insert into [dbo].[Users] (Id, FirstName, LastName, Email)
VALUES (NEWID(),'Ivan','Bićanić','ibicanic@live.com')

insert into dbo.Products (Id, ProductName, ProductDescription, ProductPrice, ProductQuantity)
VALUES (NEWID(),'Jabuka','jelo',16.8,2)

insert into dbo.Products (Id, ProductName, ProductDescription, ProductPrice, ProductQuantity)
VALUES (NEWID(),'Kruška','jelo',10.8,3)

insert into dbo.PaymentTypes (Id, PaymentMethodName)
VALUES (NEWID(), 'Kartično')

insert into [dbo].[Currencies] (Id, Code)
VALUES (NEWID(),'Euro')
select * from dbo.OrderStatuses

//STATUSE JE POTREBNO točno ovako unijeti jer ovise o konstantama !!
insert into dbo.OrderStatuses (Id, StatusName, Description)
VALUES ('CFD306D7-C7F3-47E4-A738-B34FF23EDE6B','Active','aktivan')

insert into dbo.OrderStatuses (Id, StatusName, Description)
VALUES ('896FE641-2A83-4031-9D48-18805A187D49','Confirmed','potvrđen')

insert into dbo.OrderStatuses (Id, StatusName, Description)
VALUES ('FDA40C83-2107-4CBA-AB2F-575D43288C75','Cancelled','otkazan')

Testiranje inserta Ordera može biti malo naporno za napisati, pa šaljem body:
naravno u url stavi još i userId
{
    "Products":[
        {
            "ProductId": "5626EECA-A4D0-4888-8DB3-1706D1FA1E89",
            "quantity": 2
        }
    ],
    "UserComment": "This is a user comment number 4",
    "Contact": "0912933933",
    "DeliveryAddress": "Ulica Vladimira 3",
    "PaymentTypeId": "7D19A6F4-CD02-4A0E-A65B-D37FFB8C0492",
    "CurrencyId": "0FD66C5A-9B69-4C17-957C-406C65E7044E"

}

