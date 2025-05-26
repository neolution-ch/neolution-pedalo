import { faker } from "@faker-js/faker";

/**
 * generates a valid password (uppercase, lowercase, number, special character)
 * @returns generated password
 */
const fakerValidPassword = (): string => faker.internet.password(20, true, /[A-Za-z]/, "1*A");

export { fakerValidPassword };
