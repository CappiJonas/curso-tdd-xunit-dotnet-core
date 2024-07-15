import { Selector } from "testcafe";
import Curso from "./pagesmodel/curso";

const curso = new Curso();

fixture('Curso')
    .page(curso.url)

test('Deve criar um novo curso', async t => {
    await t
        .typeText(curso.inputNome, `Curso TestCafé ${new Date().toString()}`)
        .typeText(curso.inputDescricao, 'Descrição do Curso TestCafé')
        .typeText(curso.inputCargaHoraria, '10')
        .click(curso.selectPublicoAlvo)
        .click(curso.optionEmpregado)
        .click(curso.valueOnline)
        .typeText(curso.inputValor, '1000');

    await t
        .click(curso.salvar);
        
    await t
        .expect(curso.titlePage.innerText).eql('Listagem de cursos - CursoOnline.Web')
});

test('Deve validar campos obrigatórios', async t =>{
    await t
        .click(curso.salvar);

    await t
        .expect(curso.nomeError.count).eql(1)
        .expect(curso.descricaoError.count).eql(1);
});