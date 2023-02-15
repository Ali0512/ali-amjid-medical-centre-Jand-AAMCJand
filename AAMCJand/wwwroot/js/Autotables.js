
function downloadPDF() {
    var pdf = new jsPDF('p', 'pt', 'letter');

    // Select the table element
    var table = document.getElementById("userData");

    // Get the table's headers
    var headers = [];
    for (var i = 0; i < table.rows[0].cells.length; i++) {
        headers[i] = table.rows[0].cells[i].innerHTML.toUpperCase();
    }

    // Create an array of the data in the table
    var data = [];
    for (var i = 1; i < table.rows.length; i++) {
        var row = [];
        for (var j = 0; j < table.rows[i].cells.length; j++) {
            row.push(table.rows[i].cells[j].innerHTML);
        }
        data.push(row);
    }

    // Use jsPDF's autoTable method to add the table to the PDF
    pdf.autoTable({
        head: [headers],
        body: data,
        startY: 20,
        pageBreak: 'auto',
        theme: 'grid'
    });

    // Save the PDF
    pdf.save('table.pdf');
}