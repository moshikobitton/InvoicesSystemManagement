  export const apiUrl = "https://proj.ruppin.ac.il/cgroup1/test2/tar4/api/invoices";

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
          resolve(result);
        })
        .catch((error) => {
          reject(error);
        });
    });
  };