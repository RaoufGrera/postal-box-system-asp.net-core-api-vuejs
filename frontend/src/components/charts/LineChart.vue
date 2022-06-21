<script>
  //Importing Line class from the vue-chartjs wrapper
  import { Doughnut   } from 'vue-chartjs'

  //Exporting this so it can be used in other components
  export default {
    extends: Doughnut ,
    data () {
      return {
        datacollection: {
          //Data to be represented on x-axis
          labels: ['January', 'February', 'March'],
          datasets: [
            {
              label: 'Data One',
     
              //Data to be represented on y-axis
              data: [40, 20, 30],
                 backgroundColor: [
                "#FF6384",
                "#36A2EB",
                "#FFCE56"
            ],
            }
          ]
        },
        //Chart.js options that controls the appearance of the chart
      
      }
    },
    mounted () {
      //renderChart function renders the chart with the datacollection and options object.
      this.renderChart(this.datacollection, this.options)
    }
  }
</script>