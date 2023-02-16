Feature: TFL


@TFLTests
Scenario: Journey results shown for valid journey
	Given I set valid from location
	And I set valid to location
	When I click plan my journey button
	Then I am taken to journey results screen

	Scenario: No journey results shown for invalid journeys
    Given I set an invalid from location
	And I set an invalid to location
	When I click plan my journey button
	Then I am shown an error message 
	
	Scenario: Not taken to journey results screen when no location is entered
	Given I don't set from location
	And I don't set to location
	When I click plan my journey button
	Then I see an error message on screen

	Scenario: User can edit journey from journey results page
	Given I am on journey results screen
	When I click edit journey button
	Then I can update my journey location
		
	Scenario: Recents tab display previously planned journeys
	Given There is a previous planned journey
	And I select Recents tab 
	Then I can see my recently planned journeys

