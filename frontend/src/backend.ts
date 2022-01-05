import axios from 'axios';

export const registerUser = () => {

}


export class Backend {

    private readonly _url: string;

    constructor() {
        this._url = "http://localhost:5000"
    }

    auth(cb: () => {}) {
        let xhr = new XMLHttpRequest();
        xhr.open('POST', '/api/user/auth');
        xhr.onload = cb;
        xhr.send();
    }
}
