# :rocket: Setting up
#### Clone the repo and adjust the connction string Server variable in *appsettings.json* if needed. Admin account will be automatically seeded:
##### Email - admin@abv.bg
##### Password - 123123123
#### Note - DropBox access key will be required for the image upload functionality to work

# ⌨️ Tech Stack
#### .NET7, ASP.NET Core MVC, SSMS, DropBox API, jQuery, ajax


## Homepage :page_facing_up:
#### Featured collection - shows the Makes of the cars, which were most recently added/edited. It prioritizes those that have cars with pictures uploaded in them (example - 4 cars were updated in the following order - Toyota, Ford, Mercedes, Suzuki. All of them contain models with pictures except the Ford Make. Even though Ford was updated more recently than the Toyota, the Featured Collections will be Toyota, Mercedes and Suzuki, because it prioritizes images).
![HomePage](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/b81eb8ab-e3a5-44cd-a8c0-7c9d26653078)


## Models Page :page_facing_up:
#### Models of the selected Make.
![ModelsPage](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/1413d05a-5dd8-4f11-a32d-4910dc38a468)

#### Search bar to filter models of the selected Make.
![Search](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/573ad180-0ebe-4f28-a820-d286ba72ec45)

#### Back-end pagination, which loads only the requested amount of data.
![Pagination](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/ca46f2bc-7a08-43b0-84b5-78ab7f50d518)

## Admin Panel :page_facing_up:
#### Sidebar.
![AdminPanel_Sidebar](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/fb2ff681-eb96-413e-bfe7-29453e477afa)

#### Manage All Cars - Displaying all cars, allowing AJAX CRUD operations for Admin authorized user.
![AdminPanel_ManageAllCars](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/4283e26d-ecb7-4977-b909-93f59bcf1302)

#### Add or Edit Car Modal
![AdminPanel_AddOrEditCar](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/7821641b-4a51-41c7-a745-dd1d7d0f65ca)
