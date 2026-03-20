class Info {
    constructor(Name, Age) {
    this.Name = Name;
    this.Age = Age;
    }   
} 
class SignInRequest {
    constructor(Application, Password) {
    this.Application = Application;
    this.Password = Password;
    }
}
class Mediator {
    constructor(url){
        this.url = url || "http://localhost:8081/api/";
    }
    send(msg) {
        let type = msg.constructor.name;
        let body = JSON.stringify(this.to_dict(msg));
        console.log(`Sending ${type} request: ${body}` );
        let response = this.client(type, body);
        return response;
    }
    to_dict(obj) {
        return Object.keys(obj).map(key => ({
            n: key,
            v: obj[key],
            // type: typeof obj[key]
        }));
    }
    client(type, body) {
        var xmlHttp = new XMLHttpRequest();
        xmlHttp.open( "POST", this.url + "mediator.send", false ); // false for synchronous request
        xmlHttp.setRequestHeader("Type", type);
        xmlHttp.setRequestHeader("Content-Type", "application/json");
        xmlHttp.send( body );
        return JSON.parse(xmlHttp.responseText);
    }
}
var mediator = new Mediator();
mediator.send(new SignInRequest("test", "test", new Info("John Doe", 30)));
