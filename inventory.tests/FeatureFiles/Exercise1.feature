Feature: Exercise1
    Try to complete all 3 Tasks by creating a script using any
    (if not all) of the following: C#, Specflow, Selenium, Jmeter, Postman.
    Please share your solution via Github or similar.

@ui
Scenario: Task 1 - Login to Unleashed and navigate to Add Product page and create a new product
    Given user logs into Unleashed
    And navigates to Inventory.Products.Add Product
    When add product page required fields are populated with random values
    And unit of measure EA is selected
    And product group Furniture is selected
    Then save button is clicked
    When displayed nav link View Products is clicked
    And created product is searched
    Then created product is found and verified

@ui
Scenario: Task 2 - Create a complete Sales Order flow and verify the available stock on hand
    Given user logs into Unleashed
    And navigates to Inventory.Products.View Products
    Given COUCH3,DININGCHAIR has available stocks in different warehouses
    And navigates to Sales.Orders.Add Sales Order
    When sales order for customer code BENEMP created
    And Main Warehouse selected as product sale source
    And COUCH3 qty 1 product added into sales order lines
    And DININGCHAIR qty 1 product added into sales order lines
    Then sales order Placed
    And sales order Completed
    When navigates to Inventory.Products.View Products
    And COUCH3,DININGCHAIR was rechecked in different warehouses
    Then stocks are updated

@api
Scenario: Task 3 - Call Products API endpoint and create a product
    Given user can access products API
    Then user is able to get 200 OK response code on product group, unit of measures and product list
    And existing product code DININGCHAIR is found in the product list
    When a new product is created with Furniture product group, EA unit of measure
    Then a new product is created
    When a product is updated with description 'updated via api'
    Then product is updated

#@api
#Scenario: Task 3 - Call Sales Order API endpoint and create a product ## not finished.

