import React, { createContext, useState } from "react";

export const InvoicesContext = createContext();

export default function Context({ children }) {
  const [invoices, setInvoices] = useState([]);
  const [open, setOpen] = useState(false);

  return (
    <InvoicesContext.Provider value={{ invoices, setInvoices, open, setOpen }}>
      {children}
    </InvoicesContext.Provider>
  );
}
