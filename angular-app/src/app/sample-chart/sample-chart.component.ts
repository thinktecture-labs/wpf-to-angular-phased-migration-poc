import { Component, OnInit } from '@angular/core';
import 'chartjs-adapter-date-fns';
import { ChartConfiguration } from 'chart.js';
import { IComponentSample } from '../component-sample.service';

interface ICefSharp {
  BindObjectAsync(name: string): Promise<any>;
}

declare let CefSharp: ICefSharp;

interface IChartBoundObject {
  registerSetChartData(callback: (allSamples: IComponentSample[], currentSample: IComponentSample) => void): void;
}

declare let chartBoundObject: IChartBoundObject;

@Component({
  selector: 'app-sample-chart',
  templateUrl: './sample-chart.component.html',
  styleUrls: ['./sample-chart.component.scss']
})
export class SampleChartComponent implements OnInit {

  chartData?: ChartConfiguration<'scatter', { x: string, y: number }[]>['data'];
  chartOptions: ChartConfiguration<'scatter'>['options'] = {
    responsive: true,
    scales: {
      xAxis: {
        position: 'bottom',
        type: 'time',
        time: {
          parser: 'HH:mm:ss',
          unit: 'second',
          displayFormats: {
            second: "HH:mm:ss"
          }
        },
        title: {
          display: true,
          text: 'Migration Time'
        }
      },

      yAxis: {
        type: 'logarithmic',
        min: 0.1,
        max: 10000000,
        title: {
          display: true,
          text: "RFU"
        }
      }

    }
  }

  ngOnInit(): void {
    this.tryBindToWpfHost();
  }

  private async tryBindToWpfHost(): Promise<any> {
    if (typeof (CefSharp) === "undefined") {
      this.chartData = this.createSampleChartData();
      return;
    }

    await CefSharp.BindObjectAsync('chartBoundObject');
    chartBoundObject.registerSetChartData((allSamples, currentSample) => this.createChartData(allSamples, currentSample));
  }

  private createChartData(allSamples: IComponentSample[], currentSample: IComponentSample): ChartConfiguration<'scatter', { x: string, y: number }[]>['data'] {
    const otherSamplesData = [];

    for (const sample of allSamples) {
      if (currentSample.id === sample.id)
        continue;

      otherSamplesData.push({ x: sample.migrationTime, y: sample.peakArea });
    }

    const currentSampleData = [{ x: currentSample.migrationTime, y: currentSample.peakArea }];

    return {
      datasets: [
        { data: otherSamplesData, pointRadius: 3, pointBackgroundColor: '#999999' },
        { data: currentSampleData, pointRadius: 7, pointBackgroundColor: '#ff8200' }
      ]
    }

  }

  private createSampleChartData(): ChartConfiguration<'scatter', { x: string, y: number }[]>['data'] {
    return {
      datasets: [
        {
          data: [
            { x: '00:00:10', y: 5000 },
            { x: '00:23:40', y: 150000 },
            { x: '00:40:20', y: 8000000 }
          ],
          pointRadius: 5,
          pointBackgroundColor: '#333'
        }
      ]
    };
  }
}
