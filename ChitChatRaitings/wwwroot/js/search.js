$(function (){
    $('form').submit(async e => {
        e.preventDefault();
        const q = $('#search').val();
        const respone = await fetch('/Feedbacks/Search2?query='+q);
        const data = await respone.json();

        //$('tbody').html('<tr><td>' + data[0].name + '</td><td>' + data[0].rate + '</td><td>' + data[0].description + '</td></tr>');


        const template = $('#template').html();
        let result = '';
        for (var item in data) {
            let row = template;
            for (key in data[item]) {
                row = row.replaceAll('{' + key + '}', data[item][key]);
                row = row.replaceAll('%7B' + key + '%7D', data[item][key]);
                console.log(template);
            }
            result += row;
        }
        $('tbody').html(result);

    })
})