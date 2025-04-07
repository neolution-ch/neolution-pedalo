export interface UpdateTokenCommand<T = unknown> {
  command: "refreshApiToken";
  params?: T;
}
