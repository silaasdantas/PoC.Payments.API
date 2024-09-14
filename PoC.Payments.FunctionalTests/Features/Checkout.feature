Feature: Checkout API

  Scenario: Create a checkout session successfully
    Given I have a valid amount to pay
    When I request a checkout session
    Then I should receive a session ID