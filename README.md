# QLab

ASP.NET API Backend for Role and Permission Management API, using JWT tokens, consistent error handling, and a comprehensive test suite numbering 40 different tests.

**How It Works:**

## How it works?

The system allows easy management of users and roles through CRUD operations. It employs predefined permissions for securing API endpoints. Users are associated with roles, which, in turn, have specific permissions. Users can create roles, assign permissions, and associate them with users, ensuring access control. For example, a user needs the `CreateUser` permission to use the `POST /users` endpoint, simplifying access and security.

## Table of Contents

- [Auth](#auth)
- [Role](#role)
- [RolePermission](#rolepermission)
- [User](#user)
- [UserRole](#userrole)



## Auth

### Register User

#### `POST /auth/register`

Create a new user by providing registration details.

##### Constants
```json
  {
    "username": "name",
    "password": "password"
  }
```
- Any new user created will have the default role of 'User'

### Login User

#### `POST /auth/login`

Authenticate a user by providing login credentials.

##### Constants

```json
  {
    "username": "balen",
    "password": "balen"
  }
```
Use these credentials for the administrator role.



## Role

### Get Role by ID

#### `GET /roles/{id}`

Retrieve a role by its unique identifier.

##### Constants

- Permission for reading a role: `ReadRole`

### Update Role

#### `PUT /roles/{id}`

Update an existing role by providing new details.

##### Constants

- Permission for updating a role: `UpdateRole`

### Delete Role

#### `DELETE /roles/{id}`

Delete a role by its unique identifier.

##### Constants

- Permission for deleting a role: `DeleteRole`

### Get All Roles

#### `GET /roles`

Retrieve a list of all roles.

##### Constants

- Permission for reading all roles: `ReadRoles`

### Create Role

#### `POST /roles`

Create a new role by providing role details.

##### Constants

- Permission for creating a role: `CreateRole`



## RolePermission

### Assign Permission to Role

#### `POST /role-permissions/{roleId}/assign/{permissionName}`

Assign permission to a role by providing the role's ID and the permission name.

##### Constants

- Permission for assigning permission to a role: `AssignPermissionToRole`

### Revoke Permission from Role

#### `POST /role-permissions/{roleId}/revoke/{permissionName}`

Revoke permission from a role by providing the role's ID and the permission name.

##### Constants

- Permission for revoking permission from a role: `RevokePermissionFromRole`



## User

### Get User by ID

#### `GET /users/{id}`

Retrieve a user by their unique identifier.

##### Constants

- Permission for reading a user: `ReadUser`

### Update User

#### `PUT /users/{id}`

Update an existing user by providing new details.

##### Constants

- Permission for updating a user: `UpdateUser`

### Delete User

#### `DELETE /users/{id}`

Delete a user by their unique identifier.

##### Constants

- Permission for deleting a user: `DeleteUser`

### Get All Users

#### `GET /users`

Retrieve a list of all users.

##### Constants

- Permission for reading all users: `ReadUsers`

### Create User

#### `POST /users`

Create a new user by providing user details.

##### Constants

- Permission for creating a user: `CreateUser`



## UserRole

### Assign Role to User

#### `POST /user-role/{userId}/assign/{roleId}`

Assign a role to a user by providing the user's ID and the role's ID.

##### Constants

- Permission for assigning a role to a user: `AssignRoleToUser`

### Revoke Role from User

#### `POST /user-role/{userId}/revoke/{roleId}`

Revoke a role from a user by providing the user's ID and the role's ID.

##### Constants

- Permission for revoking a role from a user: `RevokeRoleFromUser`

