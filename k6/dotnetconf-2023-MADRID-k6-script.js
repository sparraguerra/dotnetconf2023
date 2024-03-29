// Auto-generated by the postman-to-k6 converter
import "./libs/shim/core.js";
import "./libs/shim/urijs.js"
import { randomString } from 'https://jslib.k6.io/k6-utils/1.2.0/index.js'; 

export const options = {
  maxRedirects: 4,
  duration: '15s',
  vus: 20,
};

const Request = Symbol.for("request");
postman[Symbol.for("initial")]({
  options,
  environment: {
    BASE_URL: "http://localhost:5001"
  }
});

export default function() {
  
  postman[Request]({
    name: "Get all",
    id: "d983c046-6cb2-4520-a507-e643cf6f4a9a",
    method: "GET",
    address: "{{BASE_URL}}/todos"
  });
  
  postman[Request]({
    name: "Get by Id",
    id: "a33ce631-a7eb-4e13-99cb-ac9b965fbae5",
    method: "GET",
    address: "{{BASE_URL}}/todos/1"
  });
  
  var randomFirstName = randomString(8);
  postman[Request]({
    name: "Salutes",
    id: "35dc2dda-2b17-4595-bf62-0f5b33b3d581",
    method: "GET",
    address: `{{BASE_URL}}/salutes/${randomFirstName}`
  });
  
}
