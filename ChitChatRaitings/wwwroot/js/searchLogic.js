$(function () {
    $('form').submit(async e => {
        e.preventDefault();
        const q = $('#search').val();
        const respone = await fetch('/Feedbacks/Search2?query=' + q);
        const data = await respone.json();

        
        const template = $('#template').html();
        let result = '';
        for (var item in data) {
            let row = template;
            for (key in data[item]) {
                if (key == "createdDate") {
                    const date = data[item][key].split('T', 2);
                    const splitedDate = date[0].split('-', 3);
                    const finalDate = splitedDate[2] + '/' + splitedDate[1] + '/' + splitedDate[0];

                    const time = date[1].split('.', 1);
                    var d = finalDate + " " + time[0];
                    row = row.replaceAll('{' + key + '}', d);
                }
                else if (key == "description" && data[item][key] == null) {
                    var emptyDes = "";
                    row = row.replaceAll('{' + key + '}', emptyDes);
                }
                else {
                    row = row.replaceAll('{' + key + '}', data[item][key]);
                }
                row = row.replaceAll('%7B' + key + '%7D', data[item][key]);
            }
            result += row;
        }
        $('tbody').html(result);

    })
})