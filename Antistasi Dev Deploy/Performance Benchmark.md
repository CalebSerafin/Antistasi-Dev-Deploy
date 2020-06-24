# Performance Benchmarks
*CPU Bottleneck Test: Should consistently peak 100% Utilization.*<br/>
*DISCLAIMER: These were recorded by hand.*<br/>

| Version  | CPU                                          | No. Templates | No. Changes | PBO   | Total Time(s) | Output MB | MB/s  |
|----------|----------------------------------------------|---------------|-------------|-------|---------------|-----------|-------|
| 4.4.12.5 | CPU: Intel(R) Core(TM) i7-6700 CPU @ 3.40GHz | 100           | 100         | False | 71            | 511       | 7.197 |
| 4.4.12.5 | CPU: Intel(R) Core(TM) i7-6700 CPU @ 3.40GHz | 100           | 100         | False | 65            | 511       | 7.862 |
| 4.4.12.5 | CPU: Intel(R) Core(TM) i7-6700 CPU @ 3.40GHz | 100           | 100         | True  | 99            | 1029      | 10.40 |