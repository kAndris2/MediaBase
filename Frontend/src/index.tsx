import { createContext } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter as Router } from 'react-router-dom';
import { AppStart } from './AppStart';
import { IServiceContext } from './Interfaces/IServiceContext';
import { MovieBackendService } from './Services/Http/MovieBackendService';
import { SeriesBackendService } from './Services/Http/SeriesBackendService';

export const ServiceContext = createContext<IServiceContext>(null);

class ApplicationBase {
  private _service: IServiceContext;

  constructor() {
    this._initializeApp();
  }

  private async _initializeApp() : Promise<void> {
    await this._collectServices();
    this._render();
  }

  private async _collectServices() : Promise<void> {
    this._service = {
      movieBackendService: new MovieBackendService(),
      seriesBackendService: new SeriesBackendService()
    }
  }

  private _render() : void {
    const root = createRoot(document.getElementById('root'));

    root.render(
      <ServiceContext.Provider value={{
        movieBackendService: this._service.movieBackendService,
        seriesBackendService: this._service.seriesBackendService
      }}>

        <Router>
          <AppStart />
        </Router>

      </ServiceContext.Provider>
    );
  }
}

new ApplicationBase();