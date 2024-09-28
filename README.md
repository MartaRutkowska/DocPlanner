# DocPlanner

 To run: open in visual studio and run. There will be Swagger UI available

 Two endpoints available: 
 
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
