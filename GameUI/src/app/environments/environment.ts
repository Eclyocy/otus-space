const baseUrl = 'http://localhost:7070';
const shipBaseUrl = 'http://localhost:5051';

export const environment = {
  baseUrl: baseUrl,
  shipSignalRHubUrl: `${shipBaseUrl}/notifications-hub/`,
  apiUrl: `${baseUrl}/api/`
}
