import { InputFormValues, ResultType } from "../types/types";

class MainApi {
  private _baseUrl: string;
  private _headers: Record<string, string>;

  constructor(options: { baseUrl: string; headers: Record<string, string> }) {
    this._baseUrl = options.baseUrl;
    this._headers = options.headers;
  }

  private _getRequestResult<T>(res: Response): Promise<T> {
    if (res.ok) {
      return res.json();
    } else {
      return Promise.reject(`Ошибка: ${res.status}`);
    }
  }

  request(data: InputFormValues): Promise<ResultType> {
    return fetch(`${this._baseUrl}`, {
      method: "POST",
      headers: {
        ...this._headers,
      },
      body: JSON.stringify(data),
    }).then((res) => this._getRequestResult<ResultType>(res));
  }
}

// Создание экземпляра класса Api
const mainApi = new MainApi({
  baseUrl: "https://localhost:7104/api/clefia",
  headers: {
    "Content-Type": "application/json",
  },
});

export default mainApi;
