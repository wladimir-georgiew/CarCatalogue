# :rocket: Setting up
#### Clone the repo and adjust the connction string Server variable in *appsettings.json* if needed. Admin account will be automatically seeded:
##### Email - admin@abv.bg
##### Password - 123123123
#### Note - DropBox access key will be required for the image upload functionality to work

# ⌨️ Tech Stack
#### .NET7, ASP.NET Core MVC, SSMS, DropBox API, jQuery, ajax


## Homepage :page_facing_up:
#### Featured collection - shows the Makes of the cars, which were most recently added/edited. It prioritizes those that have cars with pictures uploaded in them (example - 4 cars were updated in the following order - Toyota, Ford, Mercedes, Suzuki. All of them contain models with pictures except the Ford Make. Even though Ford was updated more recently than the Toyota, the Featured Collections will be Toyota, Mercedes and Suzuki, because it prioritizes images).
![HomePage](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/84ef1c35-1fc4-47f1-8f5c-8238e4c92421)


## Models Page :page_facing_up:
#### Models of the selected Make.
![ModelsPage](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/182ca014-d3d4-4f13-979e-1e327522bf35)

#### Search bar to filter models of the selected Make.
![Search](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/f7d2da9b-f243-4e50-a96f-d543940a7e25)

#### Back-end pagination, which loads only the requested amount of data.
![Pagination](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/878d16d3-b982-4d66-9099-9e26581a0f96)

## Admin Panel :page_facing_up:
#### Sidebar.
![AdminPanel_Sidebar](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/d5741b33-08bb-4044-97ed-1f2cac6c3dcd)

#### Manage All Cars - Displaying all cars, allowing AJAX CRUD operations for Admin authorized user.
![AdminPanel_ManageAllCars](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/99ebba2d-d2f7-4dda-a49c-7e13e92486ca)

#### Add or Edit Car Modal
![AdminPanel_AddOrEditCar](https://github.com/wladimir-georgiew/CarCatalogue/assets/61605749/98bab5aa-c1e8-4817-9900-8ef6544cb3ec)
