document.addEventListener("DOMContentLoaded", function () {
    loadInvoice(); // Load default
});

function loadInvoice() {
    const id = document.getElementById('invoice-id-input').value;
    fetchInvoice(id);
}

async function fetchInvoice(id) {
    const loading = document.getElementById('loading');
    const container = document.getElementById('invoice-container');
    const errorDiv = document.getElementById('error');

    loading.style.display = 'block';
    container.style.display = 'none';
    errorDiv.style.display = 'none';

    try {
        const response = await fetch(`/api/invoice/${id}`);
        if (!response.ok) {
            throw new Error('Invoice not found');
        }
        const invoice = await response.json();
        renderInvoice(invoice);
    } catch (error) {
        errorDiv.innerText = error.message;
        errorDiv.style.display = 'block';
    } finally {
        loading.style.display = 'none';
    }
}

function renderInvoice(invoice) {
    document.getElementById('invoice-container').style.display = 'block';

    document.getElementById('inv-id').innerText = invoice.id;
    document.getElementById('inv-customer').innerText = invoice.customerName;

    const itemsList = document.getElementById('items-list');
    itemsList.innerHTML = '';

    let total = 0;
    invoice.items.forEach(item => {
        const div = document.createElement('div');
        div.className = 'item';
        div.innerHTML = `<span>${item.name}</span> <span>$${item.price.toFixed(2)}</span>`;
        itemsList.appendChild(div);
        total += item.price;
    });

    document.getElementById('inv-total').innerText = total.toFixed(2);
}
