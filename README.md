# PedaloWebApp
LOB web application for a Pedalo rental. Uses [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/?view=aspnetcore-3.1).

## Tasks
- CRUD Pages for Pedaloes, Customers and Bookings
- Dashboard (Create Booking, Highest-grossing Customer, Most popular Pedalo)
- More concise display for dates & times when rendering on web page
- Extend functionality and database to allow for passenger registration
  - Booking still needs always exactly 1 Customer, but can have 0 to multiple passengers
  - Register only first and last name of passengers that accompany the customer during the rental
  - Max. capacity of passengers for a Booking is given by Pedalo capacity minus 1 (Customer)