function refreshZones() {
    $.ajax({
        url: "/api/mistakes/dates/zone"
    }).then(function (data) {
        $('#z-time').text(data.timeZone);
        $('#z-culture').text(data.culture);
        $('#z-numbers').text(data.numbers);
        $('#z-dates').text(data.dates);
    });
}

function dateParseBad() {
    var original = M.Datepicker.getInstance($('#ex-1-datepicker')).toString();
    var json = new Date(original).toJSON();
    $('#date-parse-bad-sent').text(json);
    $.get("/api/mistakes/dates/parse/bad",
        { date: json },
        function (data) {
            $('#date-parse-bad').text(data);
            var date = new Date(data);
            $('#date-parse-bad-print').text(formatDateBAD(date));
            $('#date-parse-bad-print-error').text('che è diverso da ' + formatDateBAD(original));
            $('#date-parse-bad-print-error').show();
        }
    );
}

function getServerDate() {
    $.get("/api/mistakes/dates/today",
        function (data) {
            $('#date-get').text(data);
            var date = new Date(data);
            $('#date-get-print').text(formatDateBAD(date));
        }
    );
}

function numberParseBad() {
    var original = $('#ex-3-numeric').val();
    $('#number-parse-bad-sent').text(original);
    $.get("/api/mistakes/numbers/parse/bad",
        { number: original },
        function (data) {
            $('#number-parse-bad').text(data);
            $('#number-parse-bad-check').text(data + ' > 1000? => ' + (data > 1000) + '');
        }
    );
}

function classesCast() {
    var newOne = new Test();
    $('#classes-ok').text(newOne.method());

    var casted = {};
    casted = Object.assign(casted, newOne);

    console.log('instance:', newOne);
    console.log('casted:', casted);

    $('#classes-bad').text(!!casted.method ? casted.method() : 'undefined');
}

function formatDateBAD(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('/');
}


class Test {
    method() {
        return true;
    }
}