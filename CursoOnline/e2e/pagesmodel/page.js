import { Selector } from "testcafe";

export default class Page {
    constructor () {
        this.urlBase = 'localhost:5005';
        this.titlePage = Selector('title');
    }
}