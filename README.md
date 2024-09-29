# DocPlanner

 To run the app: Open in visual studio, choose project DocPlanner as start up project, build and run. There will be Swagger UI available.
 
 To run the tests: Right click on DocPlannerTests -> Run tests

--------

 There are two endpoints available: 
### GET  slots: 
provide date in DateTime format to receive list of all available slots
- date must be equal or greater than current time

### POST slots: 
provide parameters visible in swagger to take a slot
- start date must be equal or greater than current time
- end date must be greater than start date
- email must have proper form
- name, secondname, phone number cannot be empty
- comments are optional
