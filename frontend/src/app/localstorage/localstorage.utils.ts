/**
 * Define um item no localStorage.
 */
export function setLocalStorageItem<T>(key: string, data: T): void {
  try {
    const stringifiedData = JSON.stringify(data);
    localStorage.setItem(key, stringifiedData);
  } catch (error) {
    console.error('Erro ao gravar no localStorage:', error);
  }
}

/**
 * Recupera um item do localStorage.
 */
export function getLocalStorageItem<T>(key: string): T | null {
  try {
    const stringifiedData = localStorage.getItem(key);
    if (stringifiedData) {
      return JSON.parse(stringifiedData) as T;
    }
  } catch (error) {
    console.error('Erro ao ler do localStorage:', error);
  }
  return null;
}

/**
 * Atualiza um item existente no localStorage.
 */
export function updateLocalStorageItem<T>(key: string, newData: T): void {
  try {
    const stringifiedData = JSON.stringify(newData);
    localStorage.setItem(key, stringifiedData);
  } catch (error) {
    console.error('Erro ao atualizar item no localStorage:', error);
  }
}

/**
 * Remove um item do localStorage.
 */
export function removeLocalStorageItem(key: string): void {
  try {
    localStorage.removeItem(key);
  } catch (error) {
    console.error('Erro ao remover item do localStorage:', error);
  }
}
