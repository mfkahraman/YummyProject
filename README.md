Main Interface: This section is accessible to everyone and includes various features such as:
•	Products: Displaying the restaurant's menu items.
•	Chefs: Showcasing the restaurant's chefs.
•	Reservation Form: Allowing users to make reservations.
•	Message Form: Enabling users to send messages to the restaurant.
•	References: Featuring testimonials from important customers.
•	Photo Gallery: Displaying a gallery of restaurant photos.
•	Events: Listing special events that customers can book.

Admin Panel: This section is accessible only to authorized admins and includes the following functionalities:
•	CRUD Operations: Admins can create, read, update, and delete data for all sections on the main interface (Products, Chefs, Reservations, Messages, References, Photo Gallery, Events).
•	Reservations: Admins can handle reservations under the Reservations tab, which includes:
•	Pending Reservations: Admins can check and approve pending reservations.
•	Approved Reservations: Admins can view approved reservations.
•	Messages: Admins can manage messages under the Messages tab, which includes:
•	Unread Messages: Admins can check unread messages.
•	Read Messages: Admins can view and manage read messages.
•	Dropdown Lists: Data for dropdown lists is pulled from related tables to ensure consistency and accuracy.
•	Modal Structure: Admins can see full descriptions of reservations and messages by clicking the related button, thanks to the modal structure.

Some features of the project include:
•	Validation Rules: Models have validation rules to ensure data integrity.
•	Error and Success Messages: Controllers and views have the required functions to display error and success messages.
•	TempData, ModelState Errors, and ViewBags: Utilized TempData, ModelState errors, and ViewBags to manage and display messages and data effectively.
•	Authorization: Unauthorized users attempting to access the admin panel are redirected to the login screen.
•	Image Management: When adding images, each image is named with a GUID and included in the project. An image deletion infrastructure is established to remove old images when data is updated or deleted.
•	Modular Structure: By using the PartialView structure, each part of the project is made modular, resulting in a cleaner and more maintainable codebase.

Technologies used:
•	.NET MVC
•	HTML, CSS, Bootstrap, JavaScript, jQuery
•	Entity Framework (Code First)
•	PagedList NuGet (Paging)
•	Modal Structure (Message Viewing)
•	Naming with GUID
•	PartialView, PartialClass
•	Authorization, Session, Login, Logout


![AboutUs](https://github.com/user-attachments/assets/0af5d8b4-54a9-481a-b22f-b4b83d6028fa)
![Giris](https://github.com/user-attachments/assets/a626ac73-f33e-4230-b510-dc49a97395ab)
![WhyUs](https://github.com/user-attachments/assets/1f0e652b-b98f-43ed-ac9c-c6f3c1ef9ae2)
![AnaYemekler](https://github.com/user-attachments/assets/936d56da-5661-4ad0-a3bb-b11b795bc29a)
![Referanslar](https://github.com/user-attachments/assets/644cd36c-1075-4c17-9254-0f5098511e5b)
![Etkinlikler](https://github.com/user-attachments/assets/457eb742-d289-49ec-8a98-65400cd6fefc)
![Şefler](https://github.com/user-attachments/assets/ff0cda8b-c5c3-43b6-8d3c-03f59c8bd325)
![RezervasyonYap](https://github.com/user-attachments/assets/9bf12308-2b24-4873-b217-22a74f601535)
![Galeri](https://github.com/user-attachments/assets/120cf320-0896-419a-8c72-2743f31d0d33)
![Contact](https://github.com/user-attachments/assets/b0d68286-973b-42d9-838c-e65b1542c120)
![SendMessage](https://github.com/user-attachments/assets/1c5d8ef0-f745-459e-8137-17f1d75b6faf)
![AdminProducts](https://github.com/user-attachments/assets/812c114f-2a48-4af8-86ea-da0b28d7205c)
![AdminChefs](https://github.com/user-attachments/assets/61ef638c-30e9-4a00-a4ce-c8eaf3aac48c)
![UpdateProduct](https://github.com/user-attachments/assets/503a0209-8031-4f94-bd0b-5dbb6ce3ff94)
![AdminDasboard](https://github.com/user-attachments/assets/5fcbb985-7706-42d8-a007-65c7a11d3f58)
![AddProduct](https://github.com/user-attachments/assets/0ae02c33-a3c5-47e2-8321-9f18bc76c9dd)
![AdminEvents](https://github.com/user-attachments/assets/df75cb04-3b66-48ec-8c6c-31ca850ba5e8)
![PendingReservations](https://github.com/user-attachments/assets/cd39133e-0c3c-417c-af0a-33063a9bfac2)
![PendingReservationModal](https://github.com/user-attachments/assets/c2daefbd-51ee-4621-9129-b3454867eb5b)
![ApprovedReservations](https://github.com/user-attachments/assets/8dc87717-3bc2-4149-baf0-3940a1294749)
![AdminMessages](https://github.com/user-attachments/assets/9fa9055b-560f-4c24-9fe6-9d7cd4d3e19a)
![AdminAbout](https://github.com/user-attachments/assets/a19cee87-ba22-41cd-8dba-b16969a556f2)
![UpdateAdmin](https://github.com/user-attachments/assets/3648b8e5-f5d6-496d-8212-0f94ce73f272)
