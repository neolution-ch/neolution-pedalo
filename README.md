# Pedalo Web App

LOB web application for a Pedalo rental. Uses [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/?view=aspnetcore-3.1).

The app includes overview pages:

- for the pedalo fleet of the company.
- for the customers that have rent a pedalo, or are currently renting one.
- for the past and current rentals (bookings) to replicate which customer has rented which pedalo at what moment of time.

As soon as you start the app in Visual Studio, the database will be created automatically. Check the `SampleData.cs` to see how this works. If you make changes to the database, the old database will not automatically be changed. You need to delete the old one first and then restart the app.

## Before you start

Before you start, make sure you are familiar with [LINQ Method Syntax](https://www.tutorialsteacher.com/linq/linq-method-syntax).

## Tasks

### 1. Create [CRUD](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete) Pages for Pedaloes, Customers and Bookings

Start with the "update" pages. The "Edit customer" page is already included. The "Edit pedalo" page should be nearly the same. For the "Edit booking" page you need store the relationship between the two previous task.

After that continue to add the "create" and "delete" pages.
  
### 2. Replace start page with a Dashboard 

A dashboard is most likely the starting point for a user and should give the user interesting insights and provide helpful actions for his application and data. Add a "Quick Create Booking" feature so the user does not have to navigate to the Bookings menu first, but is able to create a booking right from the dashboard. Also create a display for the business' highest-grossing customers and the most popular Pedaloes with some calculated numbers.

### 3. Reasonable and uniform display format for dates & times when rendering on web page

At the moment dates & times have different formats, they should have the same format for the whole app. Also, some date & time information do not make sense everywhere. E.g. no one cares about the time of birth of a customer, so it should not be displayed.

### 4. Extend functionality and database to allow for passenger registration

Government regulators want to not only know who our customer is, but also what people have accompanied him on his pedalo ride. E.g. when the pedalo goes missing on the water. The water police needs to know how many people to look for. You need to adjust the database design for this change. Here is some advice on how the change can look like.

- Booking still needs always exactly 1 Customer, but now it can have 0 to multiple passengers
- Passengers must only registered with first and last name. 
- Max. capacity of passengers for a Booking is given by Pedalo capacity, minus 1 (the Customer)

### 5. Enhance the styling

Make the tables nicer and switch colors for each row. Align numbers on the right. Read more about [CSS](https://www.w3schools.com/Css/) and [fiddle online](https://jsfiddle.net/vintharas/ybt6k2dw/) style changing.

### 6. Create PDF as receipt

Once you have completed the boooking, create a PDF with the booking details using [QuestPDF](https://www.questpdf.com/). The library is alread integrated into the project and you find an example in the Pedaloes list.

### 7. Send an email receipt

Once you have completed the booking, send a confirmation email.

### 8. Selenium Tests

In the `PedaloWebApp.FunctionalTests` we have included a [selenium test](https://www.selenium.dev/documentation/webdriver/). Build a test that books a pedalo.