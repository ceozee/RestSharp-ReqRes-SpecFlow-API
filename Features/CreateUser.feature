Feature: Reqres Create User

Reqres Create User

@create @positive
Scenario: Successful Create User
	Given user name "Zee" with job "Tester"
	When request to create user is sent
	Then user is successfully created
