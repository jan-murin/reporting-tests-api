Feature: ReportingFeature
# tests for https://reqres.in/
	
Scenario: GetAllUsers - should succeed
	When I retrieve all users: page-'1'
	Then I see all users response values: page-'1' perPage-'6' total-'12' totalPages-'2'
	
Scenario: Create - should succeed
	When I create new user: name-'john' job-'programmer'
	Then I see create user response values: name-'john' job-'programmer' created-'today'
	
Scenario: Get - should fail - when user not found
	

Scenario: Update - should succeed
	When I create new user: name-'john' job-'programmer'
	Then I see create user response values: name-'john' job-'programmer' created-'today'