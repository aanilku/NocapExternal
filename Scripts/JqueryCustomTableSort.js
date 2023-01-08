//sorting

 
function getDate(str) {

    var ar = /(\d{2})\/(\d{2})\/(\d{4})/.exec(str)

    return new Date(
                    (+ar[3]),
                    (+ar[2]) - 1, // Careful, month starts at 0!
                    (+ar[1])

                );
};

var sorter = null;
function Commonsorting() {
    sorter = {

        order: [1, -1],
        column: 0,
        key: 'id',

        setOrder: function (k) {

            this.order = ({
                asc: [1, -1], desc: [-1, 1]
            })[k] || this.order;

            return this;
        },
        setColumn: function (c) {

            this.column = c || this.column;
            return this;
        },

        setKey: function (k) {
            this.key = k || this.key;
            return this;
        },

        sort: function (els) {

            var sortFunc = this.key;

            return els.sort(this[sortFunc]);
        },

        getValue: function (a, b) {

            return {
                a: $(a).find('td:eq(' + sorter.column + ')').text(),
                b: $(b).find('td:eq(' + sorter.column + ')').text()
            }
        },
        comp: function (val) {

            if (val.a == val.b) {
                return 0;
            }

            return val.a > val.b ?
                                    sorter.order[0] : sorter.order[1];

        },
        id: function (a, b) {

            var val = sorter.getValue(a, b);

            val.a = parseInt(val.a, 10);
            val.b = parseInt(val.b, 10);

            return sorter.comp(val);

        },

        notify: function (a, b) {

            var val = sorter.getValue(a, b);
            return sorter.comp(val);

        },
        date: function (a, b) {

            var val = sorter.getValue(a, b);

            val.a = getDate(val.a);
            val.b = getDate(val.b);

            return sorter.comp(val);

        }
    }

};
 
