/*
Highcharts JS v5.0.9 (2017-03-08)

(c) 2009-2016 Torstein Honsi

License: www.highcharts.com/license
*/
(function (w) { "object" === typeof module && module.exports ? module.exports = w : w(Highcharts) })(function (w) {
    (function (a) {
        function p(a, b, d) { this.init(a, b, d) } var u = a.each, v = a.extend, l = a.merge, r = a.splat; v(p.prototype, { init: function (a, b, d) {
            var g = this, m = g.defaultOptions; g.chart = b; g.options = a = l(m, b.angular ? { background: {}} : void 0, a); (a = a.background) && u([].concat(r(a)).reverse(), function (b) {
                var c, m = d.userOptions; c = l(g.defaultBackgroundOptions, b); b.backgroundColor && (c.backgroundColor = b.backgroundColor); c.color = c.backgroundColor;
                d.options.plotBands.unshift(c); m.plotBands = m.plotBands || []; m.plotBands !== d.options.plotBands && m.plotBands.unshift(c)
            })
        }, defaultOptions: { center: ["50%", "50%"], size: "85%", startAngle: 0 }, defaultBackgroundOptions: { className: "highcharts-pane", shape: "circle", borderWidth: 1, borderColor: "#cccccc", backgroundColor: { linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 }, stops: [[0, "#ffffff"], [1, "#e6e6e6"]] }, from: -Number.MAX_VALUE, innerRadius: 0, to: Number.MAX_VALUE, outerRadius: "105%"}
        }); a.Pane = p
    })(w); (function (a) {
        var p = a.CenteredSeriesMixin,
u = a.each, v = a.extend, l = a.map, r = a.merge, e = a.noop, b = a.Pane, d = a.pick, g = a.pInt, m = a.splat, n = a.wrap, c, k, h = a.Axis.prototype; a = a.Tick.prototype; c = { getOffset: e, redraw: function () { this.isDirty = !1 }, render: function () { this.isDirty = !1 }, setScale: e, setCategories: e, setTitle: e }; k = { defaultRadialGaugeOptions: { labels: { align: "center", x: 0, y: null }, minorGridLineWidth: 0, minorTickInterval: "auto", minorTickLength: 10, minorTickPosition: "inside", minorTickWidth: 1, tickLength: 10, tickPosition: "inside", tickWidth: 2, title: { rotation: 0 }, zIndex: 2 },
    defaultRadialXOptions: { gridLineWidth: 1, labels: { align: null, distance: 15, x: 0, y: null }, maxPadding: 0, minPadding: 0, showLastLabel: !1, tickLength: 0 }, defaultRadialYOptions: { gridLineInterpolation: "circle", labels: { align: "right", x: -3, y: -2 }, showLastLabel: !1, title: { x: 4, text: null, rotation: 90} }, setOptions: function (d) { d = this.options = r(this.defaultOptions, this.defaultRadialOptions, d); d.plotBands || (d.plotBands = []) }, getOffset: function () {
        h.getOffset.call(this); this.chart.axisOffset[this.side] = 0; this.center = this.pane.center =
p.getCenter.call(this.pane)
    }, getLinePath: function (b, f) { b = this.center; var c = this.chart, g = d(f, b[2] / 2 - this.offset); this.isCircular || void 0 !== f ? f = this.chart.renderer.symbols.arc(this.left + b[0], this.top + b[1], g, g, { start: this.startAngleRad, end: this.endAngleRad, open: !0, innerR: 0 }) : (f = this.postTranslate(this.angleRad, g), f = ["M", b[0] + c.plotLeft, b[1] + c.plotTop, "L", f.x, f.y]); return f }, setAxisTranslation: function () {
        h.setAxisTranslation.call(this); this.center && (this.transA = this.isCircular ? (this.endAngleRad - this.startAngleRad) /
(this.max - this.min || 1) : this.center[2] / 2 / (this.max - this.min || 1), this.minPixelPadding = this.isXAxis ? this.transA * this.minPointOffset : 0)
    }, beforeSetTickPositions: function () { if (this.autoConnect = this.isCircular && void 0 === d(this.userMax, this.options.max) && this.endAngleRad - this.startAngleRad === 2 * Math.PI) this.max += this.categories && 1 || this.pointRange || this.closestPointRange || 0 }, setAxisSize: function () {
        h.setAxisSize.call(this); this.isRadial && (this.center = this.pane.center = p.getCenter.call(this.pane), this.isCircular &&
(this.sector = this.endAngleRad - this.startAngleRad), this.len = this.width = this.height = this.center[2] * d(this.sector, 1) / 2)
    }, getPosition: function (b, f) { return this.postTranslate(this.isCircular ? this.translate(b) : this.angleRad, d(this.isCircular ? f : this.translate(b), this.center[2] / 2) - this.offset) }, postTranslate: function (b, f) { var d = this.chart, c = this.center; b = this.startAngleRad + b; return { x: d.plotLeft + c[0] + Math.cos(b) * f, y: d.plotTop + c[1] + Math.sin(b) * f} }, getPlotBandPath: function (b, f, c) {
        var m = this.center, t = this.startAngleRad,
h = m[2] / 2, a = [d(c.outerRadius, "100%"), c.innerRadius, d(c.thickness, 10)], q = Math.min(this.offset, 0), k = /%$/, n, e = this.isCircular; "polygon" === this.options.gridLineInterpolation ? m = this.getPlotLinePath(b).concat(this.getPlotLinePath(f, !0)) : (b = Math.max(b, this.min), f = Math.min(f, this.max), e || (a[0] = this.translate(b), a[1] = this.translate(f)), a = l(a, function (b) { k.test(b) && (b = g(b, 10) * h / 100); return b }), "circle" !== c.shape && e ? (b = t + this.translate(b), f = t + this.translate(f)) : (b = -Math.PI / 2, f = 1.5 * Math.PI, n = !0), a[0] -= q, a[2] -=
q, m = this.chart.renderer.symbols.arc(this.left + m[0], this.top + m[1], a[0], a[0], { start: Math.min(b, f), end: Math.max(b, f), innerR: d(a[1], a[0] - a[2]), open: n })); return m
    }, getPlotLinePath: function (b, f) {
        var d = this, c = d.center, g = d.chart, m = d.getPosition(b), a, h, k; d.isCircular ? k = ["M", c[0] + g.plotLeft, c[1] + g.plotTop, "L", m.x, m.y] : "circle" === d.options.gridLineInterpolation ? (b = d.translate(b)) && (k = d.getLinePath(0, b)) : (u(g.xAxis, function (b) { b.pane === d.pane && (a = b) }), k = [], b = d.translate(b), c = a.tickPositions, a.autoConnect && (c =
c.concat([c[0]])), f && (c = [].concat(c).reverse()), u(c, function (d, c) { h = a.getPosition(d, b); k.push(c ? "L" : "M", h.x, h.y) })); return k
    }, getTitlePosition: function () { var b = this.center, d = this.chart, c = this.options.title; return { x: d.plotLeft + b[0] + (c.x || 0), y: d.plotTop + b[1] - { high: .5, middle: .25, low: 0}[c.align] * b[2] + (c.y || 0)} } 
}; n(h, "init", function (g, f, a) {
    var h = f.angular, n = f.polar, t = a.isX, q = h && t, e, y = f.options, l = a.pane || 0; if (h) { if (v(this, q ? c : k), e = !t) this.defaultRadialOptions = this.defaultRadialGaugeOptions } else n && (v(this,
k), this.defaultRadialOptions = (e = t) ? this.defaultRadialXOptions : r(this.defaultYAxisOptions, this.defaultRadialYOptions)); h || n ? (this.isRadial = !0, f.inverted = !1, y.chart.zoomType = null) : this.isRadial = !1; g.call(this, f, a); q || !h && !n || (g = this.options, f.panes || (f.panes = []), this.pane = f = f.panes[l] = f.panes[l] || new b(m(y.pane)[l], f, this), f = f.options, this.angleRad = (g.angle || 0) * Math.PI / 180, this.startAngleRad = (f.startAngle - 90) * Math.PI / 180, this.endAngleRad = (d(f.endAngle, f.startAngle + 360) - 90) * Math.PI / 180, this.offset = g.offset ||
0, this.isCircular = e)
}); n(h, "autoLabelAlign", function (b) { if (!this.isRadial) return b.apply(this, [].slice.call(arguments, 1)) }); n(a, "getPosition", function (b, d, c, g, a) { var f = this.axis; return f.getPosition ? f.getPosition(c) : b.call(this, d, c, g, a) }); n(a, "getLabelPosition", function (b, c, g, a, m, h, k, n, e) {
    var f = this.axis, t = h.y, q = 20, z = h.align, y = (f.translate(this.pos) + f.startAngleRad + Math.PI / 2) / Math.PI * 180 % 360; f.isRadial ? (b = f.getPosition(this.pos, f.center[2] / 2 + d(h.distance, -25)), "auto" === h.rotation ? a.attr({ rotation: y }) :
null === t && (t = f.chart.renderer.fontMetrics(a.styles.fontSize).b - a.getBBox().height / 2), null === z && (f.isCircular ? (this.label.getBBox().width > f.len * f.tickInterval / (f.max - f.min) && (q = 0), z = y > q && y < 180 - q ? "left" : y > 180 + q && y < 360 - q ? "right" : "center") : z = "center", a.attr({ align: z })), b.x += h.x, b.y += t) : b = b.call(this, c, g, a, m, h, k, n, e); return b
}); n(a, "getMarkPath", function (b, c, d, g, a, m, h) { var f = this.axis; f.isRadial ? (b = f.getPosition(this.pos, f.center[2] / 2 + g), c = ["M", c, d, "L", b.x, b.y]) : c = b.call(this, c, d, g, a, m, h); return c })
    })(w);
    (function (a) {
        var p = a.each, u = a.noop, v = a.pick, l = a.Series, r = a.seriesType, e = a.seriesTypes; r("arearange", "area", { lineWidth: 1, marker: null, threshold: null, tooltip: { pointFormat: '\x3cspan style\x3d"color:{series.color}"\x3e\u25cf\x3c/span\x3e {series.name}: \x3cb\x3e{point.low}\x3c/b\x3e - \x3cb\x3e{point.high}\x3c/b\x3e\x3cbr/\x3e' }, trackByArea: !0, dataLabels: { align: null, verticalAlign: null, xLow: 0, xHigh: 0, yLow: 0, yHigh: 0 }, states: { hover: { halo: !1}} }, { pointArrayMap: ["low", "high"], dataLabelCollections: ["dataLabel",
"dataLabelUpper"], toYData: function (b) { return [b.low, b.high] }, pointValKey: "low", deferTranslatePolar: !0, highToXY: function (b) { var d = this.chart, g = this.xAxis.postTranslate(b.rectPlotX, this.yAxis.len - b.plotHigh); b.plotHighX = g.x - d.plotLeft; b.plotHigh = g.y - d.plotTop }, translate: function () {
    var b = this, d = b.yAxis, g = !!b.modifyValue; e.area.prototype.translate.apply(b); p(b.points, function (a) {
        var m = a.low, c = a.high, k = a.plotY; null === c || null === m ? a.isNull = !0 : (a.plotLow = k, a.plotHigh = d.translate(g ? b.modifyValue(c, a) : c, 0, 1,
0, 1), g && (a.yBottom = a.plotHigh))
    }); this.chart.polar && p(this.points, function (d) { b.highToXY(d) })
}, getGraphPath: function (b) {
    var d = [], g = [], a, n = e.area.prototype.getGraphPath, c, k, h; h = this.options; var q = this.chart.polar && !1 !== h.connectEnds, f = h.step; b = b || this.points; for (a = b.length; a--; ) c = b[a], c.isNull || q || b[a + 1] && !b[a + 1].isNull || g.push({ plotX: c.plotX, plotY: c.plotY, doCurve: !1 }), k = { polarPlotY: c.polarPlotY, rectPlotX: c.rectPlotX, yBottom: c.yBottom, plotX: v(c.plotHighX, c.plotX), plotY: c.plotHigh, isNull: c.isNull },
g.push(k), d.push(k), c.isNull || q || b[a - 1] && !b[a - 1].isNull || g.push({ plotX: c.plotX, plotY: c.plotY, doCurve: !1 }); b = n.call(this, b); f && (!0 === f && (f = "left"), h.step = { left: "right", center: "center", right: "left"}[f]); d = n.call(this, d); g = n.call(this, g); h.step = f; h = [].concat(b, d); this.chart.polar || "M" !== g[0] || (g[0] = "L"); this.graphPath = h; this.areaPath = this.areaPath.concat(b, g); h.isArea = !0; h.xMap = b.xMap; this.areaPath.xMap = b.xMap; return h
}, drawDataLabels: function () {
    var b = this.data, d = b.length, a, m = [], n = l.prototype, c = this.options.dataLabels,
k = c.align, h = c.verticalAlign, q = c.inside, f, t, e = this.chart.inverted; if (c.enabled || this._hasPointLabels) {
        for (a = d; a--; ) if (f = b[a]) t = q ? f.plotHigh < f.plotLow : f.plotHigh > f.plotLow, f.y = f.high, f._plotY = f.plotY, f.plotY = f.plotHigh, m[a] = f.dataLabel, f.dataLabel = f.dataLabelUpper, f.below = t, e ? k || (c.align = t ? "right" : "left") : h || (c.verticalAlign = t ? "top" : "bottom"), c.x = c.xHigh, c.y = c.yHigh; n.drawDataLabels && n.drawDataLabels.apply(this, arguments); for (a = d; a--; ) if (f = b[a]) t = q ? f.plotHigh < f.plotLow : f.plotHigh > f.plotLow, f.dataLabelUpper =
f.dataLabel, f.dataLabel = m[a], f.y = f.low, f.plotY = f._plotY, f.below = !t, e ? k || (c.align = t ? "left" : "right") : h || (c.verticalAlign = t ? "bottom" : "top"), c.x = c.xLow, c.y = c.yLow; n.drawDataLabels && n.drawDataLabels.apply(this, arguments)
    } c.align = k; c.verticalAlign = h
}, alignDataLabel: function () { e.column.prototype.alignDataLabel.apply(this, arguments) }, setStackedPoints: u, getSymbol: u, drawPoints: u
        })
    })(w); (function (a) { var p = a.seriesType; p("areasplinerange", "arearange", null, { getPointSpline: a.seriesTypes.spline.prototype.getPointSpline }) })(w);
    (function (a) {
        var p = a.defaultPlotOptions, u = a.each, v = a.merge, l = a.noop, r = a.pick, e = a.seriesType, b = a.seriesTypes.column.prototype; e("columnrange", "arearange", v(p.column, p.arearange, { lineWidth: 1, pointRange: null }), { translate: function () {
            var d = this, a = d.yAxis, m = d.xAxis, n = m.startAngleRad, c, k = d.chart, h = d.xAxis.isRadial, q; b.translate.apply(d); u(d.points, function (b) {
                var f = b.shapeArgs, g = d.options.minPointLength, e, x; b.plotHigh = q = a.translate(b.high, 0, 1, 0, 1); b.plotLow = b.plotY; x = q; e = r(b.rectPlotY, b.plotY) - q; Math.abs(e) <
g ? (g -= e, e += g, x -= g / 2) : 0 > e && (e *= -1, x -= e); h ? (c = b.barX + n, b.shapeType = "path", b.shapeArgs = { d: d.polarArc(x + e, x, c, c + b.pointWidth) }) : (f.height = e, f.y = x, b.tooltipPos = k.inverted ? [a.len + a.pos - k.plotLeft - x - e / 2, m.len + m.pos - k.plotTop - f.x - f.width / 2, e] : [m.left - k.plotLeft + f.x + f.width / 2, a.pos - k.plotTop + x + e / 2, e])
            })
        }, directTouch: !0, trackerGroups: ["group", "dataLabelsGroup"], drawGraph: l, crispCol: b.crispCol, drawPoints: b.drawPoints, drawTracker: b.drawTracker, getColumnMetrics: b.getColumnMetrics, animate: function () {
            return b.animate.apply(this,
arguments)
        }, polarArc: function () { return b.polarArc.apply(this, arguments) }, pointAttribs: b.pointAttribs
        })
    })(w); (function (a) {
        var p = a.each, u = a.isNumber, v = a.merge, l = a.pick, r = a.pInt, e = a.Series, b = a.seriesType, d = a.TrackerMixin; b("gauge", "line", { dataLabels: { enabled: !0, defer: !1, y: 15, borderRadius: 3, crop: !1, verticalAlign: "top", zIndex: 2, borderWidth: 1, borderColor: "#cccccc" }, dial: {}, pivot: {}, tooltip: { headerFormat: "" }, showInLegend: !1 }, { angular: !0, directTouch: !0, drawGraph: a.noop, fixedBox: !0, forceDL: !0, noSharedTooltip: !0,
            trackerGroups: ["group", "dataLabelsGroup"], translate: function () {
                var b = this.yAxis, d = this.options, a = b.center; this.generatePoints(); p(this.points, function (c) {
                    var g = v(d.dial, c.dial), h = r(l(g.radius, 80)) * a[2] / 200, m = r(l(g.baseLength, 70)) * h / 100, f = r(l(g.rearLength, 10)) * h / 100, n = g.baseWidth || 3, e = g.topWidth || 1, p = d.overshoot, x = b.startAngleRad + b.translate(c.y, null, null, null, !0); u(p) ? (p = p / 180 * Math.PI, x = Math.max(b.startAngleRad - p, Math.min(b.endAngleRad + p, x))) : !1 === d.wrap && (x = Math.max(b.startAngleRad, Math.min(b.endAngleRad,
x))); x = 180 * x / Math.PI; c.shapeType = "path"; c.shapeArgs = { d: g.path || ["M", -f, -n / 2, "L", m, -n / 2, h, -e / 2, h, e / 2, m, n / 2, -f, n / 2, "z"], translateX: a[0], translateY: a[1], rotation: x }; c.plotX = a[0]; c.plotY = a[1]
                })
            }, drawPoints: function () {
                var b = this, a = b.yAxis.center, d = b.pivot, c = b.options, k = c.pivot, h = b.chart.renderer; p(b.points, function (a) {
                    var d = a.graphic, g = a.shapeArgs, k = g.d, m = v(c.dial, a.dial); d ? (d.animate(g), g.d = k) : (a.graphic = h[a.shapeType](g).attr({ rotation: g.rotation, zIndex: 1 }).addClass("highcharts-dial").add(b.group), a.graphic.attr({ stroke: m.borderColor ||
"none", "stroke-width": m.borderWidth || 0, fill: m.backgroundColor || "#000000"
                    }))
                }); d ? d.animate({ translateX: a[0], translateY: a[1] }) : (b.pivot = h.circle(0, 0, l(k.radius, 5)).attr({ zIndex: 2 }).addClass("highcharts-pivot").translate(a[0], a[1]).add(b.group), b.pivot.attr({ "stroke-width": k.borderWidth || 0, stroke: k.borderColor || "#cccccc", fill: k.backgroundColor || "#000000" }))
            }, animate: function (b) {
                var a = this; b || (p(a.points, function (b) {
                    var d = b.graphic; d && (d.attr({ rotation: 180 * a.yAxis.startAngleRad / Math.PI }), d.animate({ rotation: b.shapeArgs.rotation },
a.options.animation))
                }), a.animate = null)
            }, render: function () { this.group = this.plotGroup("group", "series", this.visible ? "visible" : "hidden", this.options.zIndex, this.chart.seriesGroup); e.prototype.render.call(this); this.group.clip(this.chart.clipRect) }, setData: function (b, a) { e.prototype.setData.call(this, b, !1); this.processData(); this.generatePoints(); l(a, !0) && this.chart.redraw() }, drawTracker: d && d.drawTrackerPoint
        }, { setState: function (b) { this.state = b } })
    })(w); (function (a) {
        var p = a.each, u = a.noop, v = a.pick, l = a.seriesType,
r = a.seriesTypes; l("boxplot", "column", { threshold: null, tooltip: { pointFormat: '\x3cspan style\x3d"color:{point.color}"\x3e\u25cf\x3c/span\x3e \x3cb\x3e {series.name}\x3c/b\x3e\x3cbr/\x3eMaximum: {point.high}\x3cbr/\x3eUpper quartile: {point.q3}\x3cbr/\x3eMedian: {point.median}\x3cbr/\x3eLower quartile: {point.q1}\x3cbr/\x3eMinimum: {point.low}\x3cbr/\x3e' }, whiskerLength: "50%", fillColor: "#ffffff", lineWidth: 1, medianWidth: 2, states: { hover: { brightness: -.3} }, whiskerWidth: 2 }, { pointArrayMap: ["low", "q1", "median",
"q3", "high"], toYData: function (a) { return [a.low, a.q1, a.median, a.q3, a.high] }, pointValKey: "high", pointAttribs: function (a) { var b = this.options, d = a && a.color || this.color; return { fill: a.fillColor || b.fillColor || d, stroke: b.lineColor || d, "stroke-width": b.lineWidth || 0} }, drawDataLabels: u, translate: function () { var a = this.yAxis, b = this.pointArrayMap; r.column.prototype.translate.apply(this); p(this.points, function (d) { p(b, function (b) { null !== d[b] && (d[b + "Plot"] = a.translate(d[b], 0, 1, 0, 1)) }) }) }, drawPoints: function () {
    var a =
this, b = a.options, d = a.chart.renderer, g, m, n, c, k, h, q = 0, f, t, l, r, x = !1 !== a.doQuartiles, u, A = a.options.whiskerLength; p(a.points, function (e) {
    var p = e.graphic, z = p ? "animate" : "attr", y = e.shapeArgs, w = {}, C = {}, H = {}, I = e.color || a.color; void 0 !== e.plotY && (f = y.width, t = Math.floor(y.x), l = t + f, r = Math.round(f / 2), g = Math.floor(x ? e.q1Plot : e.lowPlot), m = Math.floor(x ? e.q3Plot : e.lowPlot), n = Math.floor(e.highPlot), c = Math.floor(e.lowPlot), p || (e.graphic = p = d.g("point").add(a.group), e.stem = d.path().addClass("highcharts-boxplot-stem").add(p),
A && (e.whiskers = d.path().addClass("highcharts-boxplot-whisker").add(p)), x && (e.box = d.path(void 0).addClass("highcharts-boxplot-box").add(p)), e.medianShape = d.path(void 0).addClass("highcharts-boxplot-median").add(p), w.stroke = e.stemColor || b.stemColor || I, w["stroke-width"] = v(e.stemWidth, b.stemWidth, b.lineWidth), w.dashstyle = e.stemDashStyle || b.stemDashStyle, e.stem.attr(w), A && (C.stroke = e.whiskerColor || b.whiskerColor || I, C["stroke-width"] = v(e.whiskerWidth, b.whiskerWidth, b.lineWidth), e.whiskers.attr(C)), x && (p =
a.pointAttribs(e), e.box.attr(p)), H.stroke = e.medianColor || b.medianColor || I, H["stroke-width"] = v(e.medianWidth, b.medianWidth, b.lineWidth), e.medianShape.attr(H)), h = e.stem.strokeWidth() % 2 / 2, q = t + r + h, e.stem[z]({ d: ["M", q, m, "L", q, n, "M", q, g, "L", q, c] }), x && (h = e.box.strokeWidth() % 2 / 2, g = Math.floor(g) + h, m = Math.floor(m) + h, t += h, l += h, e.box[z]({ d: ["M", t, m, "L", t, g, "L", l, g, "L", l, m, "L", t, m, "z"] })), A && (h = e.whiskers.strokeWidth() % 2 / 2, n += h, c += h, u = /%$/.test(A) ? r * parseFloat(A) / 100 : A / 2, e.whiskers[z]({ d: ["M", q - u, n, "L", q + u, n,
"M", q - u, c, "L", q + u, c]
})), k = Math.round(e.medianPlot), h = e.medianShape.strokeWidth() % 2 / 2, k += h, e.medianShape[z]({ d: ["M", t, k, "L", l, k] }))
})
}, setStackedPoints: u
})
    })(w); (function (a) {
        var p = a.each, u = a.noop, v = a.seriesType, l = a.seriesTypes; v("errorbar", "boxplot", { color: "#000000", grouping: !1, linkedTo: ":previous", tooltip: { pointFormat: '\x3cspan style\x3d"color:{point.color}"\x3e\u25cf\x3c/span\x3e {series.name}: \x3cb\x3e{point.low}\x3c/b\x3e - \x3cb\x3e{point.high}\x3c/b\x3e\x3cbr/\x3e' }, whiskerWidth: null }, { type: "errorbar",
            pointArrayMap: ["low", "high"], toYData: function (a) { return [a.low, a.high] }, pointValKey: "high", doQuartiles: !1, drawDataLabels: l.arearange ? function () { var a = this.pointValKey; l.arearange.prototype.drawDataLabels.call(this); p(this.data, function (e) { e.y = e[a] }) } : u, getColumnMetrics: function () { return this.linkedParent && this.linkedParent.columnMetrics || l.column.prototype.getColumnMetrics.call(this) } 
        })
    })(w); (function (a) {
        var p = a.correctFloat, u = a.isNumber, v = a.pick, l = a.Point, r = a.Series, e = a.seriesType, b = a.seriesTypes;
        e("waterfall", "column", { dataLabels: { inside: !0 }, lineWidth: 1, lineColor: "#333333", dashStyle: "dot", borderColor: "#333333", states: { hover: { lineWidthPlus: 0}} }, { pointValKey: "y", translate: function () {
            var a = this.options, g = this.yAxis, m, e, c, k, h, q, f, t, l, r = v(a.minPointLength, 5), x = r / 2, u = a.threshold, w = a.stacking, y; b.column.prototype.translate.apply(this); f = t = u; e = this.points; m = 0; for (a = e.length; m < a; m++) c = e[m], q = this.processedYData[m], k = c.shapeArgs, h = w && g.stacks[(this.negStacks && q < u ? "-" : "") + this.stackKey], y = this.getStackIndicator(y,
c.x), l = h ? h[c.x].points[this.index + "," + m + "," + y.index] : [0, q], c.isSum ? c.y = p(q) : c.isIntermediateSum && (c.y = p(q - t)), h = Math.max(f, f + c.y) + l[0], k.y = g.toPixels(h, !0), c.isSum ? (k.y = g.toPixels(l[1], !0), k.height = Math.min(g.toPixels(l[0], !0), g.len) - k.y) : c.isIntermediateSum ? (k.y = g.toPixels(l[1], !0), k.height = Math.min(g.toPixels(t, !0), g.len) - k.y, t = l[1]) : (k.height = 0 < q ? g.toPixels(f, !0) - k.y : g.toPixels(f, !0) - g.toPixels(f - q, !0), f += q), 0 > k.height && (k.y += k.height, k.height *= -1), c.plotY = k.y = Math.round(k.y) - this.borderWidth %
2 / 2, k.height = Math.max(Math.round(k.height), .001), c.yBottom = k.y + k.height, k.height <= r && !c.isNull ? (k.height = r, k.y -= x, c.plotY = k.y, c.minPointLengthOffset = 0 > c.y ? -x : x) : c.minPointLengthOffset = 0, k = c.plotY + (c.negative ? k.height : 0), this.chart.inverted ? c.tooltipPos[0] = g.len - k : c.tooltipPos[1] = k
        }, processData: function (b) {
            var a = this.yData, d = this.options.data, e, c = a.length, k, h, q, f, t, l; h = k = q = f = this.options.threshold || 0; for (l = 0; l < c; l++) t = a[l], e = d && d[l] ? d[l] : {}, "sum" === t || e.isSum ? a[l] = p(h) : "intermediateSum" === t || e.isIntermediateSum ?
a[l] = p(k) : (h += t, k += t), q = Math.min(h, q), f = Math.max(h, f); r.prototype.processData.call(this, b); this.dataMin = q; this.dataMax = f
        }, toYData: function (b) { return b.isSum ? 0 === b.x ? null : "sum" : b.isIntermediateSum ? 0 === b.x ? null : "intermediateSum" : b.y }, pointAttribs: function (a, g) { var d = this.options.upColor; d && !a.options.color && (a.color = 0 < a.y ? d : null); a = b.column.prototype.pointAttribs.call(this, a, g); delete a.dashstyle; return a }, getGraphPath: function () { return ["M", 0, 0] }, getCrispPath: function () {
            var b = this.data, a = b.length, e =
this.graph.strokeWidth() + this.borderWidth, e = Math.round(e) % 2 / 2, n = [], c, k, h; for (h = 1; h < a; h++) k = b[h].shapeArgs, c = b[h - 1].shapeArgs, k = ["M", c.x + c.width, c.y + b[h - 1].minPointLengthOffset + e, "L", k.x, c.y + b[h - 1].minPointLengthOffset + e], 0 > b[h - 1].y && (k[2] += c.height, k[5] += c.height), n = n.concat(k); return n
        }, drawGraph: function () { r.prototype.drawGraph.call(this); this.graph.attr({ d: this.getCrispPath() }) }, getExtremes: a.noop
        }, { getClassName: function () {
            var b = l.prototype.getClassName.call(this); this.isSum ? b += " highcharts-sum" :
this.isIntermediateSum && (b += " highcharts-intermediate-sum"); return b
        }, isValid: function () { return u(this.y, !0) || this.isSum || this.isIntermediateSum } 
        })
    })(w); (function (a) {
        var p = a.Series, u = a.seriesType, v = a.seriesTypes; u("polygon", "scatter", { marker: { enabled: !1, states: { hover: { enabled: !1}} }, stickyTracking: !1, tooltip: { followPointer: !0, pointFormat: "" }, trackByArea: !0 }, { type: "polygon", getGraphPath: function () {
            for (var a = p.prototype.getGraphPath.call(this), r = a.length + 1; r--; ) (r === a.length || "M" === a[r]) && 0 < r && a.splice(r,
0, "z"); return this.areaPath = a
        }, drawGraph: function () { this.options.fillColor = this.color; v.area.prototype.drawGraph.call(this) }, drawLegendSymbol: a.LegendSymbolMixin.drawRectangle, drawTracker: p.prototype.drawTracker, setStackedPoints: a.noop
        })
    })(w); (function (a) {
        var p = a.arrayMax, u = a.arrayMin, v = a.Axis, l = a.color, r = a.each, e = a.isNumber, b = a.noop, d = a.pick, g = a.pInt, m = a.Point, n = a.Series, c = a.seriesType, k = a.seriesTypes; c("bubble", "scatter", { dataLabels: { formatter: function () { return this.point.z }, inside: !0, verticalAlign: "middle" },
            marker: { lineColor: null, lineWidth: 1, radius: null, states: { hover: { radiusPlus: 0} }, symbol: "circle" }, minSize: 8, maxSize: "20%", softThreshold: !1, states: { hover: { halo: { size: 5}} }, tooltip: { pointFormat: "({point.x}, {point.y}), Size: {point.z}" }, turboThreshold: 0, zThreshold: 0, zoneAxis: "z"
        }, { pointArrayMap: ["y", "z"], parallelArrays: ["x", "y", "z"], trackerGroups: ["markerGroup", "dataLabelsGroup"], bubblePadding: !0, zoneAxis: "z", pointAttribs: function (b, a) {
            var c = d(this.options.marker.fillOpacity, .5); b = n.prototype.pointAttribs.call(this,
b, a); 1 !== c && (b.fill = l(b.fill).setOpacity(c).get("rgba")); return b
        }, getRadii: function (b, a, c, d) { var f, g, h, k = this.zData, e = [], m = this.options, n = "width" !== m.sizeBy, t = m.zThreshold, q = a - b; g = 0; for (f = k.length; g < f; g++) h = k[g], m.sizeByAbsoluteValue && null !== h && (h = Math.abs(h - t), a = Math.max(a - t, Math.abs(b - t)), b = 0), null === h ? h = null : h < b ? h = c / 2 - 1 : (h = 0 < q ? (h - b) / q : .5, n && 0 <= h && (h = Math.sqrt(h)), h = Math.ceil(c + h * (d - c)) / 2), e.push(h); this.radii = e }, animate: function (b) {
            var a = this.options.animation; b || (r(this.points, function (b) {
                var c =
b.graphic, d; c && c.width && (d = { x: c.x, y: c.y, width: c.width, height: c.height }, c.attr({ x: b.plotX, y: b.plotY, width: 1, height: 1 }), c.animate(d, a))
            }), this.animate = null)
        }, translate: function () { var b, c = this.data, d, g, m = this.radii; k.scatter.prototype.translate.call(this); for (b = c.length; b--; ) d = c[b], g = m ? m[b] : 0, e(g) && g >= this.minPxSize / 2 ? (d.marker = a.extend(d.marker, { radius: g, width: 2 * g, height: 2 * g }), d.dlBox = { x: d.plotX - g, y: d.plotY - g, width: 2 * g, height: 2 * g }) : d.shapeArgs = d.plotY = d.dlBox = void 0 }, alignDataLabel: k.column.prototype.alignDataLabel,
            buildKDTree: b, applyZones: b
        }, { haloPath: function (b) { return m.prototype.haloPath.call(this, 0 === b ? 0 : (this.marker ? this.marker.radius || 0 : 0) + b) }, ttBelow: !1 }); v.prototype.beforePadding = function () {
            var b = this, a = this.len, c = this.chart, k = 0, m = a, n = this.isXAxis, l = n ? "xData" : "yData", v = this.min, w = {}, y = Math.min(c.plotWidth, c.plotHeight), D = Number.MAX_VALUE, E = -Number.MAX_VALUE, F = this.max - v, B = a / F, G = []; r(this.series, function (a) {
                var f = a.options; !a.bubblePadding || !a.visible && c.options.chart.ignoreHiddenSeries || (b.allowZoomOutside =
!0, G.push(a), n && (r(["minSize", "maxSize"], function (b) { var a = f[b], c = /%$/.test(a), a = g(a); w[b] = c ? y * a / 100 : a }), a.minPxSize = w.minSize, a.maxPxSize = Math.max(w.maxSize, w.minSize), a = a.zData, a.length && (D = d(f.zMin, Math.min(D, Math.max(u(a), !1 === f.displayNegative ? f.zThreshold : -Number.MAX_VALUE))), E = d(f.zMax, Math.max(E, p(a))))))
            }); r(G, function (a) {
                var c = a[l], d = c.length, g; n && a.getRadii(D, E, a.minPxSize, a.maxPxSize); if (0 < F) for (; d--; ) e(c[d]) && b.dataMin <= c[d] && c[d] <= b.dataMax && (g = a.radii[d], k = Math.min((c[d] - v) * B - g, k),
m = Math.max((c[d] - v) * B + g, m))
            }); G.length && 0 < F && !this.isLog && (m -= a, B *= (a + k - m) / a, r([["min", "userMin", k], ["max", "userMax", m]], function (a) { void 0 === d(b.options[a[0]], b[a[1]]) && (b[a[0]] += a[2] / B) }))
        } 
    })(w); (function (a) {
        function p(b, a) {
            var d = this.chart, e = this.options.animation, n = this.group, c = this.markerGroup, k = this.xAxis.center, h = d.plotLeft, l = d.plotTop; d.polar ? d.renderer.isSVG && (!0 === e && (e = {}), a ? (b = { translateX: k[0] + h, translateY: k[1] + l, scaleX: .001, scaleY: .001 }, n.attr(b), c && c.attr(b)) : (b = { translateX: h, translateY: l,
                scaleX: 1, scaleY: 1
            }, n.animate(b, e), c && c.animate(b, e), this.animate = null)) : b.call(this, a)
        } var u = a.each, v = a.pick, l = a.seriesTypes, r = a.wrap, e = a.Series.prototype; a = a.Pointer.prototype; e.searchPointByAngle = function (b) { var a = this.chart, g = this.xAxis.pane.center; return this.searchKDTree({ clientX: 180 + -180 / Math.PI * Math.atan2(b.chartX - g[0] - a.plotLeft, b.chartY - g[1] - a.plotTop) }) }; r(e, "buildKDTree", function (b) { this.chart.polar && (this.kdByAngle ? this.searchPoint = this.searchPointByAngle : this.kdDimensions = 2); b.apply(this) });
        e.toXY = function (b) { var a, g = this.chart, e = b.plotX; a = b.plotY; b.rectPlotX = e; b.rectPlotY = a; a = this.xAxis.postTranslate(b.plotX, this.yAxis.len - a); b.plotX = b.polarPlotX = a.x - g.plotLeft; b.plotY = b.polarPlotY = a.y - g.plotTop; this.kdByAngle ? (g = (e / Math.PI * 180 + this.xAxis.pane.options.startAngle) % 360, 0 > g && (g += 360), b.clientX = g) : b.clientX = b.plotX }; l.spline && r(l.spline.prototype, "getPointSpline", function (a, d, g, e) {
            var b, c, k, h, m, f, l; this.chart.polar ? (b = g.plotX, c = g.plotY, a = d[e - 1], k = d[e + 1], this.connectEnds && (a || (a = d[d.length -
2]), k || (k = d[1])), a && k && (h = a.plotX, m = a.plotY, d = k.plotX, f = k.plotY, h = (1.5 * b + h) / 2.5, m = (1.5 * c + m) / 2.5, k = (1.5 * b + d) / 2.5, l = (1.5 * c + f) / 2.5, d = Math.sqrt(Math.pow(h - b, 2) + Math.pow(m - c, 2)), f = Math.sqrt(Math.pow(k - b, 2) + Math.pow(l - c, 2)), h = Math.atan2(m - c, h - b), m = Math.atan2(l - c, k - b), l = Math.PI / 2 + (h + m) / 2, Math.abs(h - l) > Math.PI / 2 && (l -= Math.PI), h = b + Math.cos(l) * d, m = c + Math.sin(l) * d, k = b + Math.cos(Math.PI + l) * f, l = c + Math.sin(Math.PI + l) * f, g.rightContX = k, g.rightContY = l), e ? (g = ["C", a.rightContX || a.plotX, a.rightContY || a.plotY, h || b, m ||
c, b, c], a.rightContX = a.rightContY = null) : g = ["M", b, c]) : g = a.call(this, d, g, e); return g
        }); r(e, "translate", function (a) { var b = this.chart; a.call(this); if (b.polar && (this.kdByAngle = b.tooltip && b.tooltip.shared, !this.preventPostTranslate)) for (a = this.points, b = a.length; b--; ) this.toXY(a[b]) }); r(e, "getGraphPath", function (a, d) {
            var b = this, e, n; if (this.chart.polar) {
                d = d || this.points; for (e = 0; e < d.length; e++) if (!d[e].isNull) { n = e; break } !1 !== this.options.connectEnds && void 0 !== n && (this.connectEnds = !0, d.splice(d.length, 0, d[n]));
                u(d, function (a) { void 0 === a.polarPlotY && b.toXY(a) })
            } return a.apply(this, [].slice.call(arguments, 1))
        }); r(e, "animate", p); l.column && (l = l.column.prototype, l.polarArc = function (a, d, g, e) { var b = this.xAxis.center, c = this.yAxis.len; return this.chart.renderer.symbols.arc(b[0], b[1], c - d, null, { start: g, end: e, innerR: c - v(a, c) }) }, r(l, "animate", p), r(l, "translate", function (a) {
            var b = this.xAxis, g = b.startAngleRad, e, l, c; this.preventPostTranslate = !0; a.call(this); if (b.isRadial) for (e = this.points, c = e.length; c--; ) l = e[c], a = l.barX +
g, l.shapeType = "path", l.shapeArgs = { d: this.polarArc(l.yBottom, l.plotY, a, a + l.pointWidth) }, this.toXY(l), l.tooltipPos = [l.plotX, l.plotY], l.ttBelow = l.plotY > b.center[1]
        }), r(l, "alignDataLabel", function (a, d, g, l, n, c) { this.chart.polar ? (a = d.rectPlotX / Math.PI * 180, null === l.align && (l.align = 20 < a && 160 > a ? "left" : 200 < a && 340 > a ? "right" : "center"), null === l.verticalAlign && (l.verticalAlign = 45 > a || 315 < a ? "bottom" : 135 < a && 225 > a ? "top" : "middle"), e.alignDataLabel.call(this, d, g, l, n, c)) : a.call(this, d, g, l, n, c) })); r(a, "getCoordinates",
function (a, d) { var b = this.chart, e = { xAxis: [], yAxis: [] }; b.polar ? u(b.axes, function (a) { var c = a.isXAxis, g = a.center, h = d.chartX - g[0] - b.plotLeft, g = d.chartY - g[1] - b.plotTop; e[c ? "xAxis" : "yAxis"].push({ axis: a, value: a.translate(c ? Math.PI - Math.atan2(h, g) : Math.sqrt(Math.pow(h, 2) + Math.pow(g, 2)), !0) }) }) : e = a.call(this, d); return e })
    })(w)
});