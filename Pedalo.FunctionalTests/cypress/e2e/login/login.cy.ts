describe("Login", () => {
  it("The login works", () => {
    cy.signIn("tenant-admin@tenant1.ch", "Secret1!");
  });
});

export {};
