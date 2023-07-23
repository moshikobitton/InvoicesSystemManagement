  export const apiUrl = "https://localhost:7283/api/Invoice";

  export const getInvoices = () => {
    return new Promise((resolve, reject) => {
      fetch(apiUrl, {
        method: "GET",
        headers: new Headers({
          "Content-Type": "application/json; charset=UTF-8",
          Accept: "application/json; charset=UTF-8",
        }),
      })
        .then((res) => {
          return res.json();
        })
        .then((result) => {
          result = result.map(invoice => ({Id: invoice.id, Status:invoice.status,Date:invoice.date, Amount:invoice.amount}))
          resolve(result);
        })
        .catch((error) => {
          reject(error);
        });
    });
  };