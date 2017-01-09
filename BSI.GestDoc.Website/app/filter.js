
app.filter('customFilter', function () {
    return function (items, filter, columns) {
        if (filter == "" || filter == undefined) {
            return items;
        }
        var exists = false;
        var filtered = [];
        var nameColumn = "";
        var propertyColum = [];
        var valuePropoertyColum;
        columns = columns.split("|");
        for (var i = 0; i < items.length; i++) {
            exists = false;
            for (var j = 0; j < columns.length; j++) {
                if (!exists) {
                    nameColumn = columns[j];
                    if (nameColumn.indexOf(".") > -1) {
                        propertyColum = nameColumn.split(".");
                        valuePropoertyColum = items[i][propertyColum[0]];
                        for (var k = 1; k < propertyColum.length; k++) {
                            valuePropoertyColum = valuePropoertyColum[propertyColum[k]];
                        }
                    } else {
                        valuePropoertyColum = items[i][nameColumn];
                    }
                    if (valuePropoertyColum != undefined) {
                        if (valuePropoertyColum.indexOf(filter) > -1) {
                            filtered.push(items[i]);
                            exists = true;
                        }
                    }
                }
            }
        }
        return filtered;
    };
});