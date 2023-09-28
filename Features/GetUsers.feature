Feature: Get user is reqres

Get Users in REQRES API

@mytag
Scenario: Get user by user ID
	Given enter user id "2"
	When request to get user by user id is sent
	Then details of user id "2" is displayed