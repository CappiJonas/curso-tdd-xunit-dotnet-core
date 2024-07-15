import { Selector } from "testcafe";
import Page from "./page";

export default class Curso extends Page {
    constructor () {
        super();
        this.url = `${this.urlBase}/Curso/Novo`
        this.inputNome = Selector('[name="Nome"]');
        this.inputDescricao = Selector('[name="Descricao"]');
        this.inputCargaHoraria = Selector('[name="CargaHoraria"]');
        this.selectPublicoAlvo = Selector('[name="PublicoAlvo"]');
        this.optionEmpregado = Selector('option[value="Empregado"]');
        this.valueOnline = Selector('[value="Online"]');
        this.inputValor = Selector('[name="Valor"]');
        this.salvar = Selector('.btn-success');
        this.nomeError = Selector('#Nome-error');
        this.descricaoError = Selector('#Descricao-error');
    }
}